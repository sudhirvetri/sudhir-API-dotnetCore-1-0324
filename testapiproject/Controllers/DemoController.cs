using Microsoft.AspNetCore.Mvc;
using testapiproject.MyLogging;

namespace testapiproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        //1. strongly coupled
        //2. loosely coupled 

        private readonly IMyLogger _myLogger;
        public DemoController()
        {
            _myLogger = new LogToServerMemory();
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("DemoController Index Method");
            return Ok();
        }
    }
}