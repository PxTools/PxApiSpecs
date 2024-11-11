/*
 * PxApi
 *
 * This api lets you do 2 things; Find a table(Navigation) and use a table (Table).  _Table below is added to show how tables can be described in yml._  **Table contains status code this API may return** | Status code    | Description      | Reason                      | | - -- -- --        | - -- -- -- -- --      | - -- -- -- -- -- -- -- -- -- --       | | 200            | Success          | The endpoint has delivered response for the request                      | | 400            | Bad request      | If the request is not valid | | 403            | Forbidden        | number of cells exceed the API limit | | 404            | Not found        | If the URL in request does not exist | | 429            | Too many request | Requests exceed the API time limit. Large queries should be run in sequence | | 50X            | Internal Server Error | The service might be down | 
 *
 * The version of the OpenAPI document: 2.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using PxWeb.Api2.Server.Attributes;
using PxWeb.Api2.Server.Models;

namespace PxWeb.Api2.Server.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public abstract class TableApiController : ControllerBase
    { 
        /// <summary>
        /// Get the default selection for Table by {id}.
        /// </summary>
        /// <remarks>Get information about what is selected for the table by default when no selection is made i the /data endpoint.</remarks>
        /// <param name="id">Id</param>
        /// <param name="lang">The language if the default is not what you want.</param>
        /// <response code="200">Success</response>
        /// <response code="400">Error respsone for 400</response>
        /// <response code="404">Error respsone for 404</response>
        /// <response code="429">Error respsone for 429</response>
        [HttpGet]
        [Route("/tables/{id}/defaultselection")]
        [ValidateModelState]
        [SwaggerOperation("GetDefaultSelection")]
        [SwaggerResponse(statusCode: 200, type: typeof(SelectionResponse), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(Problem), description: "Error respsone for 400")]
        [SwaggerResponse(statusCode: 404, type: typeof(Problem), description: "Error respsone for 404")]
        [SwaggerResponse(statusCode: 429, type: typeof(Problem), description: "Error respsone for 429")]
        public abstract IActionResult GetDefaultSelection([FromRoute (Name = "id")][Required]string id, [FromQuery (Name = "lang")]string? lang);

        /// <summary>
        /// Get Metadata about Table by {id}.
        /// </summary>
        /// <remarks>**Used for listing detailed information about a specific table** * List all variables and values and all other metadata needed to be able to fetch data  * Also links to where to:   + Fetch   - Where to get information about codelists  * 2 output formats   + Custom json    - JSON-stat2  </remarks>
        /// <param name="id">Id</param>
        /// <param name="lang">The language if the default is not what you want.</param>
        /// <param name="outputFormat">The format of the resulting metadata</param>
        /// <param name="defaultSelection">If metadata should be included as if default selection would have been applied. This is a technical parameter that is used by PxWeb for initial loading of tables. </param>
        /// <response code="200">Success</response>
        /// <response code="400">Error respsone for 400</response>
        /// <response code="404">Error respsone for 404</response>
        /// <response code="429">Error respsone for 429</response>
        [HttpGet]
        [Route("/tables/{id}/metadata")]
        [ValidateModelState]
        [SwaggerOperation("GetMetadataById")]
        [SwaggerResponse(statusCode: 200, type: typeof(TableMetadataResponse), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(Problem), description: "Error respsone for 400")]
        [SwaggerResponse(statusCode: 404, type: typeof(Problem), description: "Error respsone for 404")]
        [SwaggerResponse(statusCode: 429, type: typeof(Problem), description: "Error respsone for 429")]
        public abstract IActionResult GetMetadataById([FromRoute (Name = "id")][Required]string id, [FromQuery (Name = "lang")]string? lang, [FromQuery (Name = "outputFormat")]MetadataOutputFormatType? outputFormat, [FromQuery (Name = "defaultSelection")]bool? defaultSelection);

        /// <summary>
        /// Get Table by {id}.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="lang">The language if the default is not what you want.</param>
        /// <response code="200">Success</response>
        /// <response code="400">Error respsone for 400</response>
        /// <response code="404">Error respsone for 404</response>
        /// <response code="429">Error respsone for 429</response>
        [HttpGet]
        [Route("/tables/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetTableById")]
        [SwaggerResponse(statusCode: 200, type: typeof(TableResponse), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(Problem), description: "Error respsone for 400")]
        [SwaggerResponse(statusCode: 404, type: typeof(Problem), description: "Error respsone for 404")]
        [SwaggerResponse(statusCode: 429, type: typeof(Problem), description: "Error respsone for 429")]
        public abstract IActionResult GetTableById([FromRoute (Name = "id")][Required]string id, [FromQuery (Name = "lang")]string? lang);

        /// <summary>
        /// Get Codelist by {id}.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="lang">The language if the default is not what you want.</param>
        /// <response code="200">Success</response>
        /// <response code="400">Error respsone for 400</response>
        /// <response code="404">Error respsone for 404</response>
        /// <response code="429">Error respsone for 429</response>
        [HttpGet]
        [Route("/codelists/{id}")]
        [ValidateModelState]
        [SwaggerOperation("GetTableCodeListById")]
        [SwaggerResponse(statusCode: 200, type: typeof(CodeListResponse), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(Problem), description: "Error respsone for 400")]
        [SwaggerResponse(statusCode: 404, type: typeof(Problem), description: "Error respsone for 404")]
        [SwaggerResponse(statusCode: 429, type: typeof(Problem), description: "Error respsone for 429")]
        public abstract IActionResult GetTableCodeListById([FromRoute (Name = "id")][Required]string id, [FromQuery (Name = "lang")]string? lang);

        /// <summary>
        /// Get data from table by {id}.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="lang">The language if the default is not what you want.</param>
        /// <param name="valuecodes"></param>
        /// <param name="codelist"></param>
        /// <param name="outputvalues"></param>
        /// <param name="outputFormat"></param>
        /// <param name="heading">Commaseparated list of variable codes that should be placed in the heading in the resulting data</param>
        /// <param name="stub">Commaseparated list of variable codes that should be placed in the stub in the resulting data</param>
        /// <response code="200">Success</response>
        /// <response code="400">Error respsone for 400</response>
        /// <response code="403">Error respsone for 403</response>
        /// <response code="404">Error respsone for 404</response>
        /// <response code="429">Error respsone for 429</response>
        [HttpGet]
        [Route("/tables/{id}/data")]
        [ValidateModelState]
        [SwaggerOperation("GetTableData")]
        [SwaggerResponse(statusCode: 200, type: typeof(string), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(Problem), description: "Error respsone for 400")]
        [SwaggerResponse(statusCode: 403, type: typeof(Problem), description: "Error respsone for 403")]
        [SwaggerResponse(statusCode: 404, type: typeof(Problem), description: "Error respsone for 404")]
        [SwaggerResponse(statusCode: 429, type: typeof(Problem), description: "Error respsone for 429")]
        public abstract IActionResult GetTableData([FromRoute (Name = "id")][Required]string id, [FromQuery (Name = "lang")]string? lang, [FromQuery (Name = "valuecodes")]Dictionary<string, List<string>>? valuecodes, [FromQuery (Name = "codelist")]Dictionary<string, string>? codelist, [FromQuery (Name = "outputvalues")]Dictionary<string, CodeListOutputValuesType>? outputvalues, [FromQuery (Name = "outputFormat")]string? outputFormat, [FromQuery (Name = "heading")]List<string>? heading, [FromQuery (Name = "stub")]List<string>? stub);

        /// <summary>
        /// Get data from table by {id}.
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="lang">The language if the default is not what you want.</param>
        /// <param name="outputFormat"></param>
        /// <param name="variablesSelection">A selection</param>
        /// <response code="200">Success</response>
        /// <response code="400">Error respsone for 400</response>
        /// <response code="403">Error respsone for 403</response>
        /// <response code="404">Error respsone for 404</response>
        /// <response code="429">Error respsone for 429</response>
        [HttpPost]
        [Route("/tables/{id}/data")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("GetTableDataByPost")]
        [SwaggerResponse(statusCode: 200, type: typeof(string), description: "Success")]
        [SwaggerResponse(statusCode: 400, type: typeof(Problem), description: "Error respsone for 400")]
        [SwaggerResponse(statusCode: 403, type: typeof(Problem), description: "Error respsone for 403")]
        [SwaggerResponse(statusCode: 404, type: typeof(Problem), description: "Error respsone for 404")]
        [SwaggerResponse(statusCode: 429, type: typeof(Problem), description: "Error respsone for 429")]
        public abstract IActionResult GetTableDataByPost([FromRoute (Name = "id")][Required]string id, [FromQuery (Name = "lang")]string? lang, [FromQuery (Name = "outputFormat")]string? outputFormat, [FromBody]VariablesSelection? variablesSelection);

        /// <summary>
        /// Get all Tables.
        /// </summary>
        /// <param name="lang">The language if the default is not what you want.</param>
        /// <param name="query">Selects only tables that that matches a criteria which is specified by the search parameter.</param>
        /// <param name="pastDays">Selects only tables that was updated from the time of execution going back number of days stated by the parameter pastDays. Valid values for past days are integers between 1 and ?</param>
        /// <param name="includeDiscontinued">Decides if discontinued tables are included in response.</param>
        /// <param name="pageNumber">Pagination: Decides which page number to return</param>
        /// <param name="pageSize">Pagination: Decides how many tables per page</param>
        /// <response code="200">Success</response>
        [HttpGet]
        [Route("/tables")]
        [ValidateModelState]
        [SwaggerOperation("ListAllTables")]
        [SwaggerResponse(statusCode: 200, type: typeof(TablesResponse), description: "Success")]
        public abstract IActionResult ListAllTables([FromQuery (Name = "lang")]string? lang, [FromQuery (Name = "query")]string? query, [FromQuery (Name = "pastDays")]int? pastDays, [FromQuery (Name = "includeDiscontinued")]bool? includeDiscontinued, [FromQuery (Name = "pageNumber")]int? pageNumber, [FromQuery (Name = "pageSize")]int? pageSize);
    }
}
