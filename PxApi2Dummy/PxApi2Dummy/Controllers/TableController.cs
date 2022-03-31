using Microsoft.AspNetCore.Mvc;
using PxApi2Dummy.Models;

namespace PxApi2Dummy.Controllers
{
    /// <summary>
    /// Controller for the navigate endpoint
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        
        private readonly ILogger<TableController> _logger;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger"></param>
        public TableController(ILogger<TableController> logger)
        {
            _logger = logger;

        }


        /// <summary>
        /// Gets data for table
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}/metadata", Name = "GetTableM")]
        public Table Get(string id)
        {
            return SampleData.SampleTables.Get();
        }

    }
}