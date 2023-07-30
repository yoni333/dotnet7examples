
namespace server_api_rabitMQ.Services;

public class RabbitMqSettings {

    // for data validation see next link:
    // https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-7.0
    public string RabbitMQHost { get; set; }
    public string RabbitMQPort { get; set;}
    public string RabbitMQUserName { get; set; }
    public string RabbitMQPassword { get; set; }

    public string RabbitMQConnectionString {
        get {
            return $"host={RabbitMQHost};port={RabbitMQPort};username={RabbitMQUserName};password={RabbitMQPassword}";
        }
    }

}