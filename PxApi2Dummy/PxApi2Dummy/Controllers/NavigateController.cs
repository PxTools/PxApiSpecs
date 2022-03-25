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
            //Funker ikke her, får en null-exception:  string urlToThere2 = this.Url.RouteUrl("GetNavigate");
            //så måtte flytte alt ut til doInit og ned 
        }

        //OpenAPI 3.0 https://swagger.io/specification/#parameterObject
        //path parameters cant be optional, consider adding 2 separate endpoints
        //

        /*
        /// <summary>
        /// Get the menu tree or parts of it starting from root
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public MenuItem Get()
        {
            string urlToHere = this.Url.ActionLink();
        if (first)
            {
                DoInit();
            }
            return SampleData.SampleNavigate.Get(urlToHere);
        }
        */

        /// <summary>
        /// Get the menu tree or parts of it starting from id.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id?}", Name = "GetNavigate")]
        public MenuItem Get(string? id)
        {
            if (first)
            {
                DoInit();
            }

            string urlToHere = this.Url.ActionLink();
            //string urlToThere = this.Url.RouteUrl("GetConfigInfo", null, "https");
            //string urlToThere2 = this.Url.RouteUrl("GetNavigate", null, "https");

            return SampleData.SampleNavigate.Get(urlToHere, id);
        }

        private void DoInit()
        {
            string urlToThere = this.Url.RouteUrl("GetConfigInfo", null, "https");
           
            SampleData.SampleNavigate.InitSampleNavigate();
            first = false;
        }
    }
}