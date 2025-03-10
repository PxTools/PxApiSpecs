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
    public class JsonstatCategory : IEquatable<JsonstatCategory>
    {
        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#index)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#index)</value>
        [DataMember(Name="index", EmitDefaultValue=false)]
        public Dictionary<string, int> Index { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#label)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#label)</value>
        [DataMember(Name="label", EmitDefaultValue=false)]
        public Dictionary<string, string> Label { get; set; }

        /// <summary>
        /// Notes for values
        /// </summary>
        /// <value>Notes for values</value>
        [DataMember(Name="note", EmitDefaultValue=false)]
        public Dictionary<string, List<string>> Note { get; set; }

        /// <summary>
        /// Gets or Sets Child
        /// </summary>
        [DataMember(Name="child", EmitDefaultValue=false)]
        public Dictionary<string, List<string>> Child { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#unit)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#unit)</value>
        [DataMember(Name="unit", EmitDefaultValue=false)]
        public Dictionary<string, JsonstatCategoryUnitValue> Unit { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class JsonstatCategory {\n");
            sb.Append("  Index: ").Append(Index).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  Note: ").Append(Note).Append("\n");
            sb.Append("  Child: ").Append(Child).Append("\n");
            sb.Append("  Unit: ").Append(Unit).Append("\n");
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
            return obj.GetType() == GetType() && Equals((JsonstatCategory)obj);
        }

        /// <summary>
        /// Returns true if JsonstatCategory instances are equal
        /// </summary>
        /// <param name="other">Instance of JsonstatCategory to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(JsonstatCategory other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Index == other.Index ||
                    Index != null &&
                    other.Index != null &&
                    Index.SequenceEqual(other.Index)
                ) && 
                (
                    Label == other.Label ||
                    Label != null &&
                    other.Label != null &&
                    Label.SequenceEqual(other.Label)
                ) && 
                (
                    Note == other.Note ||
                    Note != null &&
                    other.Note != null &&
                    Note.SequenceEqual(other.Note)
                ) && 
                (
                    Child == other.Child ||
                    Child != null &&
                    other.Child != null &&
                    Child.SequenceEqual(other.Child)
                ) && 
                (
                    Unit == other.Unit ||
                    Unit != null &&
                    other.Unit != null &&
                    Unit.SequenceEqual(other.Unit)
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
                    if (Index != null)
                    hashCode = hashCode * 59 + Index.GetHashCode();
                    if (Label != null)
                    hashCode = hashCode * 59 + Label.GetHashCode();
                    if (Note != null)
                    hashCode = hashCode * 59 + Note.GetHashCode();
                    if (Child != null)
                    hashCode = hashCode * 59 + Child.GetHashCode();
                    if (Unit != null)
                    hashCode = hashCode * 59 + Unit.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(JsonstatCategory left, JsonstatCategory right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(JsonstatCategory left, JsonstatCategory right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
