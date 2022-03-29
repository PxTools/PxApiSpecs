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
        /// Gets the MenuItem at root
        /// </summary>
        /// <param name="expandedLevels">Number of levels that are expanded in the response. Minimun 1, but a typical value is 2 - 4 </param>
        /// <returns></returns>
        [HttpGet("tree", Name = "GetNavigateRoot")]
        public MenuItem Get(int expandedLevels=1)
        {
            return this.Get("", expandedLevels);
        }

        /// <summary>
        ///Gets the MenuItem at id.
        /// </summary>
        /// <param name="expandedLevels">Number of levels that are expanded in the response. Minimun 1, but a typical value is 2 - 4 </param>
        /// <returns></returns>
        [HttpGet("tree/{id}", Name = "GetNavigate")]
        public MenuItem Get(string id, int expandedLevels=1)
        {
            string urlToThere = this.Url.RouteUrl("GetNavigateRoot", null, "https");
            

            return SampleData.SampleNavigate.GetSampleNavigate(urlToThere).Get(id, expandedLevels);
        }

      
    }
}