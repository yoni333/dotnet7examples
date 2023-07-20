using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace jwt._Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    // [Route("api/v1/{controller}/{id}")]
    public class AuthController : Controller
    {

        [HttpGet("{username}/{password}")]
        public string Login(string username , string password ){
            Console.WriteLine("auth username: " + username + " password: " + password);
            return "";
        }
        
    }
}