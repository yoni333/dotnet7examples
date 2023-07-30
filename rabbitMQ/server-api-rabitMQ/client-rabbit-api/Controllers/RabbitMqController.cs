using Microsoft.AspNetCore.Mvc;
using server_api_rabitMQ.Models;
using server_api_rabitMQ.Services;
namespace rabitMQ.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class RabbitMqController : ControllerBase
    {
        RabbitRpcService _RabbitService;
        public RabbitMqController(RabbitRpcService rabbitService)
        {
            _RabbitService = rabbitService;
        }
        

        [HttpGet(Name = "sendRabbitMqMessage")]
        public IActionResult Get()
        {
            RequestFlags requestFlags = new RequestFlags() { GLOBAL_ID = "122", USER_ID = "325353", USER_IP = "1.2.45.643" };
            DataObject dataObject = new DataObject() {yourDataField1 ="yonatan" ,yourDataField2= "yehezkel"};
             Message message = _RabbitService.CreateMessage(requestFlags , dataObject , "addToque"  ,"121","entityNaming");
            _RabbitService.SendMessageAsync(message);
            return Ok("Hello Rabbit");
        }
    }
}