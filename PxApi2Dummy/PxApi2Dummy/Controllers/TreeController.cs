using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Models;

namespace PxApi2Dummy.Controllers
{
    /// <summary>
    /// Controller for the navigate endpoint
    /// </summary>
    [ApiController]
    [Route("navigate/[controller]")]
    public class TreeController : ControllerBase
    {
        

        private readonly ILogger<TreeController> _logger;

        private static bool first = true;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public TreeController(ILogger<TreeController> logger)
        {
            _logger = logger;
        }

        //OpenAPI 3.0 https://swagger.io/specification/#parameterObject
        //path parameters cant be optional, consider adding 2 separate endpoints


        /// <summary>Should folder expansing be put in an include part in stead of nested?
        /// The menutree starts here. Gets the MenuItem at the root node
        /// </summary>
        /// <param name="expandedLevels">Number of levels that are expanded in the response. Minimun 1, but a typical value is 2 - 4 </param>
        /// <param name="lang">Not implemented</param>
        /// <returns></returns>
        [HttpGet("", Name = "GetNavigateRoot")]
        public MenuItem Get([FromQuery] int expandedLevels = 1, [FromQuery] string? lang = null)
        {
            string urlToThere = this.Url.RouteUrl("GetNavigateRoot", null, "https");


            return SampleData.SampleNavigate.GetSampleNavigate(urlToThere).Get("", expandedLevels);
        }

        /// <summary>
        ///Gets the MenuItem for the node with the given id. 
        /// </summary>
        /// <param name="id">The id of the desired node</param>
        /// <param name="expandedLevels">Number of levels that are expanded in the response. Minimun 1, but a typical value is 2 - 4 </param>
        /// <param name="lang">Not implemented</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetNavigate")]
        public MenuItem Get([FromRoute] string id, [FromQuery] int expandedLevels=1, [FromQuery]string? lang = null)
        {
            string urlToThere = this.Url.RouteUrl("GetNavigateRoot", null, "https");
            return SampleData.SampleNavigate.GetSampleNavigate(urlToThere).Get(id, expandedLevels);
        }

    }
}