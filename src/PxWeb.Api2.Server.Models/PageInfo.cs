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
    public class PageInfo : IEquatable<PageInfo>
    {
        /// <summary>
        /// The current page number.
        /// </summary>
        /// <value>The current page number.</value>
        [Required]
        [DataMember(Name="pageNumber", EmitDefaultValue=true)]
        public int PageNumber { get; set; }

        /// <summary>
        /// The maximal number of elements in a page
        /// </summary>
        /// <value>The maximal number of elements in a page</value>
        /* <example>100</example> */
        [Required]
        [DataMember(Name="pageSize", EmitDefaultValue=true)]
        public int PageSize { get; set; }

        /// <summary>
        /// the Total number of elements
        /// </summary>
        /// <value>the Total number of elements</value>
        [Required]
        [DataMember(Name="totalElements", EmitDefaultValue=true)]
        public int TotalElements { get; set; }

        /// <summary>
        /// The total number of pages
        /// </summary>
        /// <value>The total number of pages</value>
        [Required]
        [DataMember(Name="totalPages", EmitDefaultValue=true)]
        public int TotalPages { get; set; }

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
            sb.Append("class PageInfo {\n");
            sb.Append("  PageNumber: ").Append(PageNumber).Append("\n");
            sb.Append("  PageSize: ").Append(PageSize).Append("\n");
            sb.Append("  TotalElements: ").Append(TotalElements).Append("\n");
            sb.Append("  TotalPages: ").Append(TotalPages).Append("\n");
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
            return obj.GetType() == GetType() && Equals((PageInfo)obj);
        }

        /// <summary>
        /// Returns true if PageInfo instances are equal
        /// </summary>
        /// <param name="other">Instance of PageInfo to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PageInfo other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    PageNumber == other.PageNumber ||
                    
                    PageNumber.Equals(other.PageNumber)
                ) && 
                (
                    PageSize == other.PageSize ||
                    
                    PageSize.Equals(other.PageSize)
                ) && 
                (
                    TotalElements == other.TotalElements ||
                    
                    TotalElements.Equals(other.TotalElements)
                ) && 
                (
                    TotalPages == other.TotalPages ||
                    
                    TotalPages.Equals(other.TotalPages)
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
                    
                    hashCode = hashCode * 59 + PageNumber.GetHashCode();
                    
                    hashCode = hashCode * 59 + PageSize.GetHashCode();
                    
                    hashCode = hashCode * 59 + TotalElements.GetHashCode();
                    
                    hashCode = hashCode * 59 + TotalPages.GetHashCode();
                    if (Links != null)
                    hashCode = hashCode * 59 + Links.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(PageInfo left, PageInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PageInfo left, PageInfo right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
