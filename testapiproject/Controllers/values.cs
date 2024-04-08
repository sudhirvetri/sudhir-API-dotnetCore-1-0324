using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace testapiproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Values : ControllerBase
    {
        [HttpGet]
        public string defaultoutput()
        {
            return "Hello World";
        }
    }
}
