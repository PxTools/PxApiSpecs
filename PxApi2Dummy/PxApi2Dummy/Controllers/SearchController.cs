using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Models;

namespace PxApi2Dummy.Controllers
{
    /// <summary>
    /// Controller for the navigate endpoint
    /// </summary>
    [ApiController]
    [Route("navigate/[controller]")]
    public class SearchController : ControllerBase
    {
        
        private readonly ILogger<SearchController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public SearchController(ILogger<SearchController> logger)
        {
            _logger = logger;
        }

        //OpenAPI 3.0 https://swagger.io/specification/#parameterObject
        //path parameters cant be optional, consider adding 2 separate endpoints

        [HttpGet("search", Name = "Search")]
        public MenuItem Get([FromQuery] string? searchText= "Uføre*", [FromQuery] string? lang = null)
        {
            //string urlToThere = this.Url.RouteUrl("GetNavigateRoot", null, "https");


            //return SampleData.SampleNavigate.GetSampleNavigate(urlToThere).Get("", expandedLevels);
            return null;
        }



    }
}