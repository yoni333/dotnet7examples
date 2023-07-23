using Microsoft.AspNetCore.Mvc;
using mssql.services;
namespace mssql_books.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{

    public MssqlCalls _mssqlCalls;

    public WeatherForecastController(MssqlCalls mssqlCalls) => _mssqlCalls = mssqlCalls;

    [HttpGet(Name = "GetWeatherForecast")]
   
    [HttpGet("mssql-query")]
    public IActionResult getMssql(){
        var list =_mssqlCalls.SelectTable();
        
        return Ok(new {name="ali"});
    }

    [HttpGet("mssql-call-procedure/{id}")]
    public IActionResult getMssqlProcedure(int id)
    {
        var list = _mssqlCalls.SelectProcedure(id);

        return Ok(new { name = "ali" });
    }
}
