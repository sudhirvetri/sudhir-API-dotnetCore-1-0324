using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class valuesController : ControllerBase
    {
        [HttpGet]
        public string GetValues(){
            return "12312312";
        }
    }
}
