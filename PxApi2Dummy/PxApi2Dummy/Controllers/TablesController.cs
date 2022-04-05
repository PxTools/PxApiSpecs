using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Models;
using PxApi2Dummy.Search;
using Swashbuckle.AspNetCore.Annotations;

namespace PxApi2Dummy.Controllers
{
    /// <summary>
    /// Controller for the navigate endpoint
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [SwaggerTag("Hello from TablesController!")]
    public class TablesController : ControllerBase
    {

        private readonly ILogger<TablesController> _logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public TablesController(ILogger<TablesController> logger)
        {
            _logger = logger;
        }

        //OpenAPI 3.0 https://swagger.io/specification/#parameterObject
        //path parameters cant be optional, consider adding 2 separate endpoints


        /// <summary>
        /// Paginated. List of "table-locators"
        /// </summary>
        /// <param name="searchText"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet(Name = "search")]
        public TablesResult Get([FromQuery] string? searchText = "title:uføretryg*", [FromQuery] string? lang = null)
        {
            //string urlToThere = this.Url.RouteUrl("GetNavigateRoot", null, "https");
            LangHolder myLang = new LangHolder(lang);

            return DoSearch.Get(searchText, myLang);
        }


        /// <summary>
        /// Or is it better to keep in after the ?
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet("pastdays_isthisok", Name = "search2Id")]
        public SearchResultItem Get2Id([FromRoute] string id, [FromQuery] string? lang = null)
        {
            return new SearchResultItem();
        }

        /// <summary>
        /// Parhaps not much use for this, but should be here for consistency. 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "searchId")]
        public SearchResultItem GetId([FromRoute] string id, [FromQuery] string? lang = null)
        {
            return new SearchResultItem();
        }


        /// <summary>
        /// Gets metadata for table
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/metadata", Name = "GetTableM")]
        public Response<ToDo> Get(string id)
        {
            var tmp = new ToDo { something = id };
            return new Response<ToDo> { Data = tmp };
        }

        /// <summary>
        /// Gets data for table
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/data", Name = "GetTableD")]
        public ToDo GetData(string id)
        {
            return new ToDo { something = id };
        }

        /// <summary>
        /// Paginated?. Gets list of filters for table
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/filters", Name = "GetTableF")]
        public ToDo GetFilters(string id)
        {
            return new ToDo { something = id };
        }


        /// <summary>
        /// Gets a filter for table. (  ? )
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/filters/{filterId}", Name = "GetTableFid")]
        public ToDo GetFilters(string id, string filterId)
        {
            return new ToDo { something = id + " filter: "+ filterId};
        }

    }
}