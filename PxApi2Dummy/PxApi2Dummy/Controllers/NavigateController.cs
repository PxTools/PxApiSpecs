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

        private static bool first = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public NavigateController(ILogger<NavigateController> logger)
        {
            _logger = logger;
        }

        //OpenAPI 3.0 https://swagger.io/specification/#parameterObject
        //path parameters cant be optional, consider adding 2 separate endpoints
      


        /// <summary>
        /// Get the menu tree or parts of it starting from root
        /// </summary>
        /// <returns></returns>
        [HttpGet("tree", Name = "GetNavigateRoot")]
        public MenuItem Get()
        {
            return this.Get("");
        }

        /// <summary>
        /// Get the menu tree or parts of it starting from id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("tree/{id?}", Name = "GetNavigate")]
        public MenuItem Get(string? id)
        {
            string urlToThere= this.Url.RouteUrl("GetNavigateRoot", null, "https");

            return SampleData.SampleNavigate.GetSampleNavigate(urlToThere).Get(id);
        }

      
    }
}