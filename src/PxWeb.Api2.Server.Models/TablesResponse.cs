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
    /// 
    /// </summary>
    [DataContract]
    public class TablesResponse : IEquatable<TablesResponse>
    {
        /// <summary>
        /// The language code (ISO 639) for this response
        /// </summary>
        /// <value>The language code (ISO 639) for this response</value>
        [Required]
        [DataMember(Name="language", EmitDefaultValue=false)]
        public string Language { get; set; }

        /// <summary>
        /// Gets or Sets Tables
        /// </summary>
        [Required]
        [DataMember(Name="tables", EmitDefaultValue=false)]
        public List<Table> Tables { get; set; }

        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [Required]
        [DataMember(Name="page", EmitDefaultValue=false)]
        public PageInfo Page { get; set; }

        /// <summary>
        /// Gets or Sets Links
        /// </summary>
        [DataMember(Name="links", EmitDefaultValue=false)]
        public List<Link> Links { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TablesResponse {\n");
            sb.Append("  Language: ").Append(Language).Append("\n");
            sb.Append("  Tables: ").Append(Tables).Append("\n");
            sb.Append("  Page: ").Append(Page).Append("\n");
            sb.Append("  Links: ").Append(Links).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TablesResponse)obj);
        }

        /// <summary>
        /// Returns true if TablesResponse instances are equal
        /// </summary>
        /// <param name="other">Instance of TablesResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TablesResponse other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Language == other.Language ||
                    Language != null &&
                    Language.Equals(other.Language)
                ) && 
                (
                    Tables == other.Tables ||
                    Tables != null &&
                    other.Tables != null &&
                    Tables.SequenceEqual(other.Tables)
                ) && 
                (
                    Page == other.Page ||
                    Page != null &&
                    Page.Equals(other.Page)
                ) && 
                (
                    Links == other.Links ||
                    Links != null &&
                    other.Links != null &&
                    Links.SequenceEqual(other.Links)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Language != null)
                    hashCode = hashCode * 59 + Language.GetHashCode();
                    if (Tables != null)
                    hashCode = hashCode * 59 + Tables.GetHashCode();
                    if (Page != null)
                    hashCode = hashCode * 59 + Page.GetHashCode();
                    if (Links != null)
                    hashCode = hashCode * 59 + Links.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(TablesResponse left, TablesResponse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TablesResponse left, TablesResponse right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
