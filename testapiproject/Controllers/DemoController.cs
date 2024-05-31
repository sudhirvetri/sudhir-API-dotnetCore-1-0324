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

        private readonly ILogger<DemoController> _myLogger;
        public DemoController(ILogger<DemoController> myLogger)
        {
            //_myLogger = new LogToServerMemory();
            _myLogger = myLogger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.LogTrace("Log message from trace method");
            _myLogger.LogDebug("Log message from Debug method");
            _myLogger.LogInformation("Log message from Information method");
            _myLogger.LogWarning("Log message from warning method");
            _myLogger.LogError("Log message from Error method");
            _myLogger.LogCritical("Log message from Critical method");
            return Ok();
        }
    }
}