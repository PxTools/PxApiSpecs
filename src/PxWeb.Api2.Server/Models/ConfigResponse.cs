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
    /// API configuration
    /// </summary>
    [DataContract]
    public class ConfigResponse : IEquatable<ConfigResponse>
    {
        /// <summary>
        /// The version of then API
        /// </summary>
        /// <value>The version of then API</value>
        [Required]
        [DataMember(Name="apiVersion", EmitDefaultValue=false)]
        public string ApiVersion { get; set; }

        /// <summary>
        /// A list of language that exists for the data.
        /// </summary>
        /// <value>A list of language that exists for the data.</value>
        [Required]
        [DataMember(Name="languages", EmitDefaultValue=false)]
        public List<Language> Languages { get; set; }

        /// <summary>
        /// The id of the language that is the default language.
        /// </summary>
        /// <value>The id of the language that is the default language.</value>
        [Required]
        [DataMember(Name="defaultLanguage", EmitDefaultValue=false)]
        public string DefaultLanguage { get; set; }

        /// <summary>
        /// A threshold of how many datacells that can be fetched in a single API call
        /// </summary>
        /// <value>A threshold of how many datacells that can be fetched in a single API call</value>
        [Required]
        [DataMember(Name="maxDataCells", EmitDefaultValue=true)]
        public int MaxDataCells { get; set; }

        /// <summary>
        /// The maximum number of call to the API for a time window indicated by timeWindow.
        /// </summary>
        /// <value>The maximum number of call to the API for a time window indicated by timeWindow.</value>
        [Required]
        [DataMember(Name="maxCallsPerTimeWindow", EmitDefaultValue=true)]
        public int MaxCallsPerTimeWindow { get; set; }

        /// <summary>
        /// The time window restricting how many call that can be done.
        /// </summary>
        /// <value>The time window restricting how many call that can be done.</value>
        [Required]
        [DataMember(Name="timeWindow", EmitDefaultValue=true)]
        public int TimeWindow { get; set; }

        /// <summary>
        /// The license that the data is provided.
        /// </summary>
        /// <value>The license that the data is provided.</value>
        [Required]
        [DataMember(Name="license", EmitDefaultValue=false)]
        public string License { get; set; }

        /// <summary>
        /// A list of how the data should be cite for diffrent languages.
        /// </summary>
        /// <value>A list of how the data should be cite for diffrent languages.</value>
        [DataMember(Name="sourceReferences", EmitDefaultValue=false)]
        public List<SourceReference> SourceReferences { get; set; }

        /// <summary>
        /// Gets or Sets DefaultMetadataFormat
        /// </summary>
        [Required]
        [DataMember(Name="defaultMetadataFormat", EmitDefaultValue=true)]
        public MetadataOutputFormatType DefaultMetadataFormat { get; set; }

        /// <summary>
        /// The default data format to used when no format is specified in the request.
        /// </summary>
        /// <value>The default data format to used when no format is specified in the request.</value>
        [Required]
        [DataMember(Name="defaultDataFormat", EmitDefaultValue=false)]
        public string DefaultDataFormat { get; set; }

        /// <summary>
        /// List of available data formts for fetching data in.
        /// </summary>
        /// <value>List of available data formts for fetching data in.</value>
        [Required]
        [DataMember(Name="dataFormats", EmitDefaultValue=false)]
        public List<string> DataFormats { get; set; }

        /// <summary>
        /// A list of features for the API
        /// </summary>
        /// <value>A list of features for the API</value>
        [DataMember(Name="features", EmitDefaultValue=false)]
        public List<ApiFeature> Features { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ConfigResponse {\n");
            sb.Append("  ApiVersion: ").Append(ApiVersion).Append("\n");
            sb.Append("  Languages: ").Append(Languages).Append("\n");
            sb.Append("  DefaultLanguage: ").Append(DefaultLanguage).Append("\n");
            sb.Append("  MaxDataCells: ").Append(MaxDataCells).Append("\n");
            sb.Append("  MaxCallsPerTimeWindow: ").Append(MaxCallsPerTimeWindow).Append("\n");
            sb.Append("  TimeWindow: ").Append(TimeWindow).Append("\n");
            sb.Append("  License: ").Append(License).Append("\n");
            sb.Append("  SourceReferences: ").Append(SourceReferences).Append("\n");
            sb.Append("  DefaultMetadataFormat: ").Append(DefaultMetadataFormat).Append("\n");
            sb.Append("  DefaultDataFormat: ").Append(DefaultDataFormat).Append("\n");
            sb.Append("  DataFormats: ").Append(DataFormats).Append("\n");
            sb.Append("  Features: ").Append(Features).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ConfigResponse)obj);
        }

        /// <summary>
        /// Returns true if ConfigResponse instances are equal
        /// </summary>
        /// <param name="other">Instance of ConfigResponse to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ConfigResponse other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    ApiVersion == other.ApiVersion ||
                    ApiVersion != null &&
                    ApiVersion.Equals(other.ApiVersion)
                ) && 
                (
                    Languages == other.Languages ||
                    Languages != null &&
                    other.Languages != null &&
                    Languages.SequenceEqual(other.Languages)
                ) && 
                (
                    DefaultLanguage == other.DefaultLanguage ||
                    DefaultLanguage != null &&
                    DefaultLanguage.Equals(other.DefaultLanguage)
                ) && 
                (
                    MaxDataCells == other.MaxDataCells ||
                    
                    MaxDataCells.Equals(other.MaxDataCells)
                ) && 
                (
                    MaxCallsPerTimeWindow == other.MaxCallsPerTimeWindow ||
                    
                    MaxCallsPerTimeWindow.Equals(other.MaxCallsPerTimeWindow)
                ) && 
                (
                    TimeWindow == other.TimeWindow ||
                    
                    TimeWindow.Equals(other.TimeWindow)
                ) && 
                (
                    License == other.License ||
                    License != null &&
                    License.Equals(other.License)
                ) && 
                (
                    SourceReferences == other.SourceReferences ||
                    SourceReferences != null &&
                    other.SourceReferences != null &&
                    SourceReferences.SequenceEqual(other.SourceReferences)
                ) && 
                (
                    DefaultMetadataFormat == other.DefaultMetadataFormat ||
                    
                    DefaultMetadataFormat.Equals(other.DefaultMetadataFormat)
                ) && 
                (
                    DefaultDataFormat == other.DefaultDataFormat ||
                    DefaultDataFormat != null &&
                    DefaultDataFormat.Equals(other.DefaultDataFormat)
                ) && 
                (
                    DataFormats == other.DataFormats ||
                    DataFormats != null &&
                    other.DataFormats != null &&
                    DataFormats.SequenceEqual(other.DataFormats)
                ) && 
                (
                    Features == other.Features ||
                    Features != null &&
                    other.Features != null &&
                    Features.SequenceEqual(other.Features)
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
                    if (ApiVersion != null)
                    hashCode = hashCode * 59 + ApiVersion.GetHashCode();
                    if (Languages != null)
                    hashCode = hashCode * 59 + Languages.GetHashCode();
                    if (DefaultLanguage != null)
                    hashCode = hashCode * 59 + DefaultLanguage.GetHashCode();
                    
                    hashCode = hashCode * 59 + MaxDataCells.GetHashCode();
                    
                    hashCode = hashCode * 59 + MaxCallsPerTimeWindow.GetHashCode();
                    
                    hashCode = hashCode * 59 + TimeWindow.GetHashCode();
                    if (License != null)
                    hashCode = hashCode * 59 + License.GetHashCode();
                    if (SourceReferences != null)
                    hashCode = hashCode * 59 + SourceReferences.GetHashCode();
                    
                    hashCode = hashCode * 59 + DefaultMetadataFormat.GetHashCode();
                    if (DefaultDataFormat != null)
                    hashCode = hashCode * 59 + DefaultDataFormat.GetHashCode();
                    if (DataFormats != null)
                    hashCode = hashCode * 59 + DataFormats.GetHashCode();
                    if (Features != null)
                    hashCode = hashCode * 59 + Features.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ConfigResponse left, ConfigResponse right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ConfigResponse left, ConfigResponse right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
