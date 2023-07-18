using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace jwt._Controllers
{

    [ApiController]
    [Route("[controller]")]
    // [Route("api/v1/{controller}/{id}")]
    public class AuthController : Controller
    {

        [HttpGet]
        // [Route("api/v1/s/{id2?}")]
        public string Get(int? id , int? id2 ){
            ;

            return "auth id:" + id + " id2: " + id2;
        }
        
    }
}