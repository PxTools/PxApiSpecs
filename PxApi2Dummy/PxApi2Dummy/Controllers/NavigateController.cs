using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Data;

namespace PxApi2Dummy.Controllers
{
    /// <summary>
    /// Controller for the navigate endpoint
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NavigateController : ControllerBase
    {
        

        private readonly ILogger<NavigateController> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public NavigateController(ILogger<NavigateController> logger)
        {
            _logger = logger;

        }
        /// <summary>
        /// Get the menu tree or parts of it
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetNavigate")]
        public IEnumerable<MenuItem> Get()
        {
            return SampleData.SampleNavigate.Get();
        }

    }
}