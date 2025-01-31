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
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PxWeb.Api2.Server.Converters;

namespace PxWeb.Api2.Server.Models
{ 
        /// <summary>
        /// Gets or Sets MetadataOutputFormatType
        /// </summary>
        [TypeConverter(typeof(CustomEnumConverter<MetadataOutputFormatType>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum MetadataOutputFormatType
        {
            
            /// <summary>
            /// Enum JsonPxEnum for json-px
            /// </summary>
            [EnumMember(Value = "json-px")]
            JsonPxEnum = 1,
            
            /// <summary>
            /// Enum JsonStat2Enum for json-stat2
            /// </summary>
            [EnumMember(Value = "json-stat2")]
            JsonStat2Enum = 2
        }
}
