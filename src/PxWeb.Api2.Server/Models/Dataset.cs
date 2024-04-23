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
    /// Representation of TableMetaData/TableData according to JSON-stat 2.0 Dataset Schema (2018-09-05 10:55), see full specification of JSON-stat format [here](https://json-stat.org/full/)  Properties in **extension** are mostly from PX-file format, see [PX file format](https://www.scb.se/en/services/statistical-programs-for-px-files/px-file-format/) 
    /// </summary>
    [DataContract]
    public class Dataset : IEquatable<Dataset>
    {

        /// <summary>
        /// JSON-stat version 2.0
        /// </summary>
        /// <value>JSON-stat version 2.0</value>
        [TypeConverter(typeof(CustomEnumConverter<VersionEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum VersionEnum
        {
            
            /// <summary>
            /// Enum _20Enum for 2.0
            /// </summary>
            [EnumMember(Value = "2.0")]
            _20Enum = 1
        }

        /// <summary>
        /// JSON-stat version 2.0
        /// </summary>
        /// <value>JSON-stat version 2.0</value>
        [Required]
        [DataMember(Name="version", EmitDefaultValue=true)]
        public VersionEnum _Version { get; set; } = VersionEnum._20Enum;


        /// <summary>
        /// Is always dataset
        /// </summary>
        /// <value>Is always dataset</value>
        [TypeConverter(typeof(CustomEnumConverter<ClassEnum>))]
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public enum ClassEnum
        {
            
            /// <summary>
            /// Enum DatasetEnum for dataset
            /// </summary>
            [EnumMember(Value = "dataset")]
            DatasetEnum = 1
        }

        /// <summary>
        /// Is always dataset
        /// </summary>
        /// <value>Is always dataset</value>
        [Required]
        [DataMember(Name="class", EmitDefaultValue=true)]
        public ClassEnum Class { get; set; } = ClassEnum.DatasetEnum;

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#href)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#href)</value>
        [DataMember(Name="href", EmitDefaultValue=false)]
        public string Href { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#label)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#label)</value>
        [DataMember(Name="label", EmitDefaultValue=false)]
        public string Label { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#source)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#source)</value>
        [DataMember(Name="source", EmitDefaultValue=false)]
        public string Source { get; set; }

        /// <summary>
        /// See https://json-stat.org/full/#updated
        /// </summary>
        /// <value>See https://json-stat.org/full/#updated</value>
        [RegularExpression("^((19|20)\\d\\d)\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$")]
        [DataMember(Name="updated", EmitDefaultValue=false)]
        public string Updated { get; set; }

        /// <summary>
        /// Gets or Sets Link
        /// </summary>
        [DataMember(Name="link", EmitDefaultValue=false)]
        public Dictionary<string, List<JsonstatLink>> Link { get; set; }

        /// <summary>
        /// Spesification on json-stat.org -&gt; [here](https://json-stat.org/full/#note)
        /// </summary>
        /// <value>Spesification on json-stat.org -&gt; [here](https://json-stat.org/full/#note)</value>
        [DataMember(Name="note", EmitDefaultValue=false)]
        public List<string> Note { get; set; }

        /// <summary>
        /// Gets or Sets Role
        /// </summary>
        [DataMember(Name="role", EmitDefaultValue=false)]
        public DatasetRole Role { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [Required]
        [DataMember(Name="id", EmitDefaultValue=false)]
        public List<string> Id { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#size)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#size)</value>
        [Required]
        [DataMember(Name="size", EmitDefaultValue=false)]
        public List<int> Size { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#dimension)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#dimension)</value>
        [Required]
        [DataMember(Name="dimension", EmitDefaultValue=false)]
        public Dictionary<string, DatasetDimensionValue> Dimension { get; set; }

        /// <summary>
        /// Gets or Sets Extension
        /// </summary>
        [DataMember(Name="extension", EmitDefaultValue=false)]
        public ExtensionRoot Extension { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#value)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#value)</value>
        [Required]
        [DataMember(Name="value", EmitDefaultValue=true)]
        public List<double?> Value { get; set; }

        /// <summary>
        /// Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#status)
        /// </summary>
        /// <value>Specification on json-stat.org -&gt; [here](https://json-stat.org/full/#status)</value>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public Dictionary<string, string> Status { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Dataset {\n");
            sb.Append("  _Version: ").Append(_Version).Append("\n");
            sb.Append("  Class: ").Append(Class).Append("\n");
            sb.Append("  Href: ").Append(Href).Append("\n");
            sb.Append("  Label: ").Append(Label).Append("\n");
            sb.Append("  Source: ").Append(Source).Append("\n");
            sb.Append("  Updated: ").Append(Updated).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  Note: ").Append(Note).Append("\n");
            sb.Append("  Role: ").Append(Role).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Size: ").Append(Size).Append("\n");
            sb.Append("  Dimension: ").Append(Dimension).Append("\n");
            sb.Append("  Extension: ").Append(Extension).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
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
            return obj.GetType() == GetType() && Equals((Dataset)obj);
        }

        /// <summary>
        /// Returns true if Dataset instances are equal
        /// </summary>
        /// <param name="other">Instance of Dataset to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Dataset other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    _Version == other._Version ||
                    
                    _Version.Equals(other._Version)
                ) && 
                (
                    Class == other.Class ||
                    
                    Class.Equals(other.Class)
                ) && 
                (
                    Href == other.Href ||
                    Href != null &&
                    Href.Equals(other.Href)
                ) && 
                (
                    Label == other.Label ||
                    Label != null &&
                    Label.Equals(other.Label)
                ) && 
                (
                    Source == other.Source ||
                    Source != null &&
                    Source.Equals(other.Source)
                ) && 
                (
                    Updated == other.Updated ||
                    Updated != null &&
                    Updated.Equals(other.Updated)
                ) && 
                (
                    Link == other.Link ||
                    Link != null &&
                    other.Link != null &&
                    Link.SequenceEqual(other.Link)
                ) && 
                (
                    Note == other.Note ||
                    Note != null &&
                    other.Note != null &&
                    Note.SequenceEqual(other.Note)
                ) && 
                (
                    Role == other.Role ||
                    Role != null &&
                    Role.Equals(other.Role)
                ) && 
                (
                    Id == other.Id ||
                    Id != null &&
                    other.Id != null &&
                    Id.SequenceEqual(other.Id)
                ) && 
                (
                    Size == other.Size ||
                    Size != null &&
                    other.Size != null &&
                    Size.SequenceEqual(other.Size)
                ) && 
                (
                    Dimension == other.Dimension ||
                    Dimension != null &&
                    other.Dimension != null &&
                    Dimension.SequenceEqual(other.Dimension)
                ) && 
                (
                    Extension == other.Extension ||
                    Extension != null &&
                    Extension.Equals(other.Extension)
                ) && 
                (
                    Value == other.Value ||
                    Value != null &&
                    other.Value != null &&
                    Value.SequenceEqual(other.Value)
                ) && 
                (
                    Status == other.Status ||
                    Status != null &&
                    other.Status != null &&
                    Status.SequenceEqual(other.Status)
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
                    
                    hashCode = hashCode * 59 + _Version.GetHashCode();
                    
                    hashCode = hashCode * 59 + Class.GetHashCode();
                    if (Href != null)
                    hashCode = hashCode * 59 + Href.GetHashCode();
                    if (Label != null)
                    hashCode = hashCode * 59 + Label.GetHashCode();
                    if (Source != null)
                    hashCode = hashCode * 59 + Source.GetHashCode();
                    if (Updated != null)
                    hashCode = hashCode * 59 + Updated.GetHashCode();
                    if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                    if (Note != null)
                    hashCode = hashCode * 59 + Note.GetHashCode();
                    if (Role != null)
                    hashCode = hashCode * 59 + Role.GetHashCode();
                    if (Id != null)
                    hashCode = hashCode * 59 + Id.GetHashCode();
                    if (Size != null)
                    hashCode = hashCode * 59 + Size.GetHashCode();
                    if (Dimension != null)
                    hashCode = hashCode * 59 + Dimension.GetHashCode();
                    if (Extension != null)
                    hashCode = hashCode * 59 + Extension.GetHashCode();
                    if (Value != null)
                    hashCode = hashCode * 59 + Value.GetHashCode();
                    if (Status != null)
                    hashCode = hashCode * 59 + Status.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Dataset left, Dataset right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Dataset left, Dataset right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
