using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace PxApi2Dummy.Controllers
{
    /// <summary>
    /// Controller for the navigate endpoint
    /// </summary>
    [ApiController]
    [Route("navigate/[controller]")]
    [SwaggerTag("Endpoints for getting the menutree.")]
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


        /// <summary>Should folder expanding be solved by putting elements in an include part in stead of nested?
        /// The menutree starts here. Gets the MenuItem at the root node
        /// </summary>
        /// <param name="expandedLevels">Number of levels that are expanded in the response. Minimun 1, but a typical value is 2 - 4 </param>
        /// <param name="lang">Not implemented</param>
        /// <returns></returns>
        [HttpGet("", Name = "GetNavigateRoot")]
        public Response<MenuItem> Get([FromQuery] int expandedLevels = 1, [FromQuery] string? lang = null)
        {
            string urlToThere = this.Url.RouteUrl("GetNavigateRoot", null, "https");


            return new Response<MenuItem> { Data = SampleData.SampleNavigate.GetSampleNavigate(urlToThere).Get("", expandedLevels) };
        }

        /// <summary>
        ///Gets the MenuItem for the node with the given id. 
        /// </summary>
        /// <param name="id">The id of the desired node</param>
        /// <param name="expandedLevels">Number of levels that are expanded in the response. Minimun 1, but a typical value is 2 - 4 </param>
        /// <param name="lang">Not implemented</param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetNavigate")]
        public Response<MenuItem> Get([FromRoute] string id, [FromQuery] int expandedLevels=1, [FromQuery]string? lang = null)
        {
            string urlToThere = this.Url.RouteUrl("GetNavigateRoot", null, "https");
            return new Response<MenuItem> { Data = SampleData.SampleNavigate.GetSampleNavigate(urlToThere).Get(id, expandedLevels) };
        }

    }
}