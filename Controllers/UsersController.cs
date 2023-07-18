
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace jwt._Controllers
{
    public class MyApiControllerAttribute : Attribute, IRouteTemplateProvider
    {
        public string Template => "api/[controller]/{id}";
        public int? Order => 2;  //learn more about order of url match values : -1 , 0 , 1 , 2
        public string Name { get; set; } = string.Empty;
    }
    /*
    Route attribute ara concatenating. so if you will uncomment the next 2 attributes
    you will need to add /my-app/ to all of your request url's here
    [ApiController]
    [Route("[my-app]")]

    */
    public class UsersController : Controller
    {

        //http://{{host}}/users
        [Route("[controller]")]
        public string Index()
        {
            return "i am users index method";
        }
        // the [action] token is the method name
        //http://{{host}}/users/index2
        [Route("[controller]/[action]")]
        public string Index2()
        {
            return "i am users index 2 method";
        }
        //http://{{host}}/users/index3/1/2
        [Route("[controller]/[action]/{id}/{id2?}")]
        public string Index3(int id, int id2)
        {
            return "i am users index 3 method id:" + id + " id2:" + id2;
        }
        //http://{{host}}/index4
        [Route("index4")]
        public string Index4()
        {
            return "i am users index 4 method ";
        }
        //http://{{host}}/index5/1/2
        //http://{{host}}/index5/1
        // string  converted to zero or to optional if exist because method int id type--  http://{{host}}/index5/q/g
        [Route("index5/{id}/{id2?}")]
        public string Index5(int id, int id2 = 5)
        {
            return "i am users index 5 method id:" + id + " id2 optional:" + id2;
        }
        //http://{{host}}/index5a/
        //http://{{host}}/index5a/1/
        //http://{{host}}/index5a/1/2
        [Route("index5a/{id?}/{id2?}")]
        public string Index5a(int id, int id2 = 5)
        {
            return "i am users index 5a method id optional:" + id + " id2 optional:" + id2;
        }
        //multiple  routes
        //http://{{host}}/index6/1
        //http://{{host}}/index6a/2
        //http://{{host}}/home/3
        //http://{{host}}/users/Index6multi/2
        //http://{{host}}/users/Index6multi
        [Route("index6/{id}")]
        [Route("index6a/{id}")]
        [Route("HOME/{id}")]
        [Route("[controller]/[action]/{id?}")]

        public string Index6multi(int id)
        {
            return "i am users index 6 or 6a or home,  method id:" + id ;
        }
        //custom attribute
        //http://{{host}}/api/users/2
        [MyApiController]
        public string Index7(int id)
        {
            return "i am users index 7 with custom attribute,  method id:" + id ;
        }
        //constrain string length -- see more at https://learn.microsoft.com/en-us/aspnet/core/fundamentals/routing?view=aspnetcore-7.0#route-template-referenc
        //http://{{host}}/api/users/2
        [Route("[controller]/[action]/{id:maxlength(8)}")]
        public string Index8(string id)
        {
            return "i am users index 8 with type var,  method id:" + id ;
        }
        // http attribute are just Route attribute extension and accept the same options
        [HttpGet("[controller]/[action]/{id}")]
        public string Index9(string id)
        {
            return "i am users index 9 with HTTP REST verb GET,  method id:" + id;
        }
        
        [HttpPost("[controller]/[action]/{id}")]
        public string Index10(string id)
        {
            return "i am users index 10 with HTTP REST verb POST,  method id:" + id;
        }

    }
}