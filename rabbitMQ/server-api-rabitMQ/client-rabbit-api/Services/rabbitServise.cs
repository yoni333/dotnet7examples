

using Microsoft.Extensions.Options;
using System.Collections.Concurrent;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using server_api_rabitMQ.Models;

namespace server_api_rabitMQ.Services
{
    public class RabbitRpcService : IDisposable
    {

        private const string QUEUE_NAME = "rpc_queue";
        private const string EXCHANGE_NAME = "rpc_exchange";


        private readonly IConnection connection;
        private readonly IModel channel;
        private readonly string replyQueueName;
        private readonly ConcurrentDictionary<string, TaskCompletionSource<string>> callbackMapper = new();

        private readonly IOptions<RabbitMqSettings> _rabbitMqSettings;
        public RabbitRpcService(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings;
            Console.WriteLine("RabbitMQ Settings: " + _rabbitMqSettings.Value.RabbitMQConnectionString);
            // System.Diagnostics.Debug.WriteLine("RabbitMQ Settings: " + _rabbitMqSettings.Value.RabbitMQConnectionString);
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };

                connection = factory.CreateConnection();
                channel = connection.CreateModel();
                // declare a server-named queue
                replyQueueName = channel.QueueDeclare().QueueName;
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    if (!callbackMapper.TryRemove(ea.BasicProperties.CorrelationId, out var tcs))
                        return;
                    var body = ea.Body.ToArray();
                    var response = Encoding.UTF8.GetString(body);
                    tcs.TrySetResult(response);
                };

                channel.BasicConsume(consumer: consumer,
                                     queue: replyQueueName,
                                     autoAck: true);

            }
            catch (Exception err)
            {


                Console.WriteLine(err);
                System.Diagnostics.Debug.WriteLine(err);
                // setTimeout(connectRabbitMQ, 10000);
            }
        }
        public Message CreateMessage(RequestFlags request, DataObject dataObject, string operationName, string entityId, string entityName)
        {

            var ip = request.USER_IP;
            var userName = request.USER_ID;
            var requestId = request.GLOBAL_ID;
            Message msg = new Message()
            {
                username = userName,
                entityName = "Form",
                ip = ip,
                data = JsonSerializer.Serialize(dataObject),
                requestId = requestId,
                actionDate = new DateTime().ToString(),
                entityId = entityId.Length > 0  ? entityId : "-1",
                operation = operationName,
                system = "NE",
            };
            Console.WriteLine(" Sending to Audit: user: " + userName + ", opertion: " + operationName);

            return msg;
        }

        public Task<string> SendMessageAsync(Message message, CancellationToken cancellationToken = default)
        {
            IBasicProperties props = channel.CreateBasicProperties();
            var correlationId = Guid.NewGuid().ToString();
            props.CorrelationId = correlationId;
            props.ReplyTo = replyQueueName;
            var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
            var tcs = new TaskCompletionSource<string>();
            callbackMapper.TryAdd(correlationId, tcs);

            channel.BasicPublish(exchange: EXCHANGE_NAME,
                                 routingKey: QUEUE_NAME,
                                 basicProperties: props,
                                 body: messageBytes);

            cancellationToken.Register(() => callbackMapper.TryRemove(correlationId, out _));
            return tcs.Task;
        }

        public void Dispose()
        {
            connection.Close();
            Console.WriteLine("RabbitMQ connection closed -excahnge ." + EXCHANGE_NAME);
        }


    }

}