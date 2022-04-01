using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Models;


namespace PxApi2Dummy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigInfoController : ControllerBase
    {
        

        private readonly ILogger<ConfigInfoController> _logger;

        public ConfigInfoController(ILogger<ConfigInfoController> logger)
        {
            _logger = logger;            
        }

        [HttpGet(Name = "GetConfigInfo")]
        public ConfigInfo Get()
        {
            return SampleData.SampleConfigInfo.Get();
        }

    }
}