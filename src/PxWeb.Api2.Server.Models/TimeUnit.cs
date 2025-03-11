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
        /// Indicates the time scale for the variable.
        /// </summary>
        /// <value>Indicates the time scale for the variable.</value>
        [TypeConverter(typeof(CustomEnumConverter<TimeUnit>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum TimeUnit
        {
            
            /// <summary>
            /// Enum AnnualEnum for Annual
            /// </summary>
            [EnumMember(Value = "Annual")]
            AnnualEnum = 1,
            
            /// <summary>
            /// Enum QuarterlyEnum for Quarterly
            /// </summary>
            [EnumMember(Value = "Quarterly")]
            QuarterlyEnum = 2,
            
            /// <summary>
            /// Enum MonthlyEnum for Monthly
            /// </summary>
            [EnumMember(Value = "Monthly")]
            MonthlyEnum = 3,
            
            /// <summary>
            /// Enum WeeklyEnum for Weekly
            /// </summary>
            [EnumMember(Value = "Weekly")]
            WeeklyEnum = 4,
            
            /// <summary>
            /// Enum OtherEnum for Other
            /// </summary>
            [EnumMember(Value = "Other")]
            OtherEnum = 5
        }
}
