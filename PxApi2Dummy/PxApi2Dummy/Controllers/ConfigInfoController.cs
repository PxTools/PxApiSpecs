using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PxApi2Dummy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Endpoints for getting information about config.")]
    public class ConfigInfoController : ControllerBase
    {

        private readonly ILogger<ConfigInfoController> _logger;

        public ConfigInfoController(ILogger<ConfigInfoController> logger)
        {
            _logger = logger;            
        }

        [HttpGet(Name = "GetConfigInfo")]
        public Response<ConfigInfo> Get()
        {
            return new Response<ConfigInfo> { Data = SampleData.SampleConfigInfo.Get() };
        }

    }
}