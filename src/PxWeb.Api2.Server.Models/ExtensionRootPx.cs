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
    /// Properties corresponds to keywords in the px-file.  See [PX file format](https://www.scb.se/en/services/statistical-programs-for-px-files/px-file-format/) 
    /// </summary>
    [DataContract]
    public class ExtensionRootPx : IEquatable<ExtensionRootPx>
    {
        /// <summary>
        /// Name of a file containing more information for the statistics**
        /// </summary>
        /// <value>Name of a file containing more information for the statistics**</value>
        [DataMember(Name="infofile", EmitDefaultValue=false)]
        public string? Infofile { get; set; }

        /// <summary>
        /// A text that is the identity of the table
        /// </summary>
        /// <value>A text that is the identity of the table</value>
        [DataMember(Name="tableid", EmitDefaultValue=false)]
        public string? Tableid { get; set; }

        /// <summary>
        /// The number of decimals in the table cells
        /// </summary>
        /// <value>The number of decimals in the table cells</value>
        [DataMember(Name="decimals", EmitDefaultValue=true)]
        public int? Decimals { get; set; }

        /// <summary>
        /// Indicates if the data table is included in the official statistics of the organization
        /// </summary>
        /// <value>Indicates if the data table is included in the official statistics of the organization</value>
        [DataMember(Name="official-statistics", EmitDefaultValue=true)]
        public bool? OfficialStatistics { get; set; }

        /// <summary>
        /// If the contents of the table cannot be aggregated
        /// </summary>
        /// <value>If the contents of the table cannot be aggregated</value>
        [DataMember(Name="aggregallowed", EmitDefaultValue=true)]
        public bool? Aggregallowed { get; set; }

        /// <summary>
        /// If the table is protected by copyright
        /// </summary>
        /// <value>If the table is protected by copyright</value>
        [DataMember(Name="copyright", EmitDefaultValue=true)]
        public bool? Copyright { get; set; }

        /// <summary>
        /// code (two characters) for language
        /// </summary>
        /// <value>code (two characters) for language</value>
        [DataMember(Name="language", EmitDefaultValue=false)]
        public string? Language { get; set; }

        /// <summary>
        /// Information about the contents, which makes up the first part of a title created when retrieving tables from PC-Axis.
        /// </summary>
        /// <value>Information about the contents, which makes up the first part of a title created when retrieving tables from PC-Axis.</value>
        [DataMember(Name="contents", EmitDefaultValue=false)]
        public string? Contents { get; set; }

        /// <summary>
        /// See _description_ in [PX file format](https://www.scb.se/en/services/statistical-programs-for-px-files/px-file-format/)
        /// </summary>
        /// <value>See _description_ in [PX file format](https://www.scb.se/en/services/statistical-programs-for-px-files/px-file-format/)</value>
        [DataMember(Name="description", EmitDefaultValue=false)]
        public string? Description { get; set; }

        /// <summary>
        /// For some languages it is difficult to build a table title dynamically. The keyword descriptiondefault &#x3D; True; means that the text after keyword Description will be used as title for the table
        /// </summary>
        /// <value>For some languages it is difficult to build a table title dynamically. The keyword descriptiondefault &#x3D; True; means that the text after keyword Description will be used as title for the table</value>
        [DataMember(Name="descriptiondefault", EmitDefaultValue=true)]
        public bool? Descriptiondefault { get; set; }

        /// <summary>
        /// List of suggested variables for table head
        /// </summary>
        /// <value>List of suggested variables for table head</value>
        [DataMember(Name="heading", EmitDefaultValue=false)]
        public List<string> Heading { get; set; }

        /// <summary>
        /// List of suggested variables for table stub
        /// </summary>
        /// <value>List of suggested variables for table stub</value>
        [DataMember(Name="stub", EmitDefaultValue=false)]
        public List<string> Stub { get; set; }

        /// <summary>
        /// The name of the matrix
        /// </summary>
        /// <value>The name of the matrix</value>
        [DataMember(Name="matrix", EmitDefaultValue=false)]
        public string? Matrix { get; set; }

        /// <summary>
        /// Subject area code
        /// </summary>
        /// <value>Subject area code</value>
        [DataMember(Name="subject-code", EmitDefaultValue=false)]
        public string? SubjectCode { get; set; }

        /// <summary>
        /// Subject area
        /// </summary>
        /// <value>Subject area</value>
        [DataMember(Name="subject-area", EmitDefaultValue=false)]
        public string? SubjectArea { get; set; }

        /// <summary>
        /// See https://json-stat.org/full/#updated
        /// </summary>
        /// <value>See https://json-stat.org/full/#updated</value>
        [RegularExpression("^((19|20)\\d\\d)\\-(0?[1-9]|1[012])\\-(0?[1-9]|[12][0-9]|3[01])$")]
        [DataMember(Name="nextUpdate", EmitDefaultValue=false)]
        public string? NextUpdate { get; set; }

        /// <summary>
        /// Survey for table
        /// </summary>
        /// <value>Survey for table</value>
        [DataMember(Name="survey", EmitDefaultValue=false)]
        public string? Survey { get; set; }

        /// <summary>
        /// Links for tables
        /// </summary>
        /// <value>Links for tables</value>
        [DataMember(Name="link", EmitDefaultValue=false)]
        public string? Link { get; set; }

        /// <summary>
        /// How often a table is updated
        /// </summary>
        /// <value>How often a table is updated</value>
        [DataMember(Name="updateFrequency", EmitDefaultValue=false)]
        public string? UpdateFrequency { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ExtensionRootPx {\n");
            sb.Append("  Infofile: ").Append(Infofile).Append("\n");
            sb.Append("  Tableid: ").Append(Tableid).Append("\n");
            sb.Append("  Decimals: ").Append(Decimals).Append("\n");
            sb.Append("  OfficialStatistics: ").Append(OfficialStatistics).Append("\n");
            sb.Append("  Aggregallowed: ").Append(Aggregallowed).Append("\n");
            sb.Append("  Copyright: ").Append(Copyright).Append("\n");
            sb.Append("  Language: ").Append(Language).Append("\n");
            sb.Append("  Contents: ").Append(Contents).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  Descriptiondefault: ").Append(Descriptiondefault).Append("\n");
            sb.Append("  Heading: ").Append(Heading).Append("\n");
            sb.Append("  Stub: ").Append(Stub).Append("\n");
            sb.Append("  Matrix: ").Append(Matrix).Append("\n");
            sb.Append("  SubjectCode: ").Append(SubjectCode).Append("\n");
            sb.Append("  SubjectArea: ").Append(SubjectArea).Append("\n");
            sb.Append("  NextUpdate: ").Append(NextUpdate).Append("\n");
            sb.Append("  Survey: ").Append(Survey).Append("\n");
            sb.Append("  Link: ").Append(Link).Append("\n");
            sb.Append("  UpdateFrequency: ").Append(UpdateFrequency).Append("\n");
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
            return obj.GetType() == GetType() && Equals((ExtensionRootPx)obj);
        }

        /// <summary>
        /// Returns true if ExtensionRootPx instances are equal
        /// </summary>
        /// <param name="other">Instance of ExtensionRootPx to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ExtensionRootPx other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Infofile == other.Infofile ||
                    Infofile != null &&
                    Infofile.Equals(other.Infofile)
                ) && 
                (
                    Tableid == other.Tableid ||
                    Tableid != null &&
                    Tableid.Equals(other.Tableid)
                ) && 
                (
                    Decimals == other.Decimals ||
                    
                    Decimals.Equals(other.Decimals)
                ) && 
                (
                    OfficialStatistics == other.OfficialStatistics ||
                    
                    OfficialStatistics.Equals(other.OfficialStatistics)
                ) && 
                (
                    Aggregallowed == other.Aggregallowed ||
                    
                    Aggregallowed.Equals(other.Aggregallowed)
                ) && 
                (
                    Copyright == other.Copyright ||
                    
                    Copyright.Equals(other.Copyright)
                ) && 
                (
                    Language == other.Language ||
                    Language != null &&
                    Language.Equals(other.Language)
                ) && 
                (
                    Contents == other.Contents ||
                    Contents != null &&
                    Contents.Equals(other.Contents)
                ) && 
                (
                    Description == other.Description ||
                    Description != null &&
                    Description.Equals(other.Description)
                ) && 
                (
                    Descriptiondefault == other.Descriptiondefault ||
                    
                    Descriptiondefault.Equals(other.Descriptiondefault)
                ) && 
                (
                    Heading == other.Heading ||
                    Heading != null &&
                    other.Heading != null &&
                    Heading.SequenceEqual(other.Heading)
                ) && 
                (
                    Stub == other.Stub ||
                    Stub != null &&
                    other.Stub != null &&
                    Stub.SequenceEqual(other.Stub)
                ) && 
                (
                    Matrix == other.Matrix ||
                    Matrix != null &&
                    Matrix.Equals(other.Matrix)
                ) && 
                (
                    SubjectCode == other.SubjectCode ||
                    SubjectCode != null &&
                    SubjectCode.Equals(other.SubjectCode)
                ) && 
                (
                    SubjectArea == other.SubjectArea ||
                    SubjectArea != null &&
                    SubjectArea.Equals(other.SubjectArea)
                ) && 
                (
                    NextUpdate == other.NextUpdate ||
                    NextUpdate != null &&
                    NextUpdate.Equals(other.NextUpdate)
                ) && 
                (
                    Survey == other.Survey ||
                    Survey != null &&
                    Survey.Equals(other.Survey)
                ) && 
                (
                    Link == other.Link ||
                    Link != null &&
                    Link.Equals(other.Link)
                ) && 
                (
                    UpdateFrequency == other.UpdateFrequency ||
                    UpdateFrequency != null &&
                    UpdateFrequency.Equals(other.UpdateFrequency)
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
                    if (Infofile != null)
                    hashCode = hashCode * 59 + Infofile.GetHashCode();
                    if (Tableid != null)
                    hashCode = hashCode * 59 + Tableid.GetHashCode();
                    
                    hashCode = hashCode * 59 + Decimals.GetHashCode();
                    
                    hashCode = hashCode * 59 + OfficialStatistics.GetHashCode();
                    
                    hashCode = hashCode * 59 + Aggregallowed.GetHashCode();
                    
                    hashCode = hashCode * 59 + Copyright.GetHashCode();
                    if (Language != null)
                    hashCode = hashCode * 59 + Language.GetHashCode();
                    if (Contents != null)
                    hashCode = hashCode * 59 + Contents.GetHashCode();
                    if (Description != null)
                    hashCode = hashCode * 59 + Description.GetHashCode();
                    
                    hashCode = hashCode * 59 + Descriptiondefault.GetHashCode();
                    if (Heading != null)
                    hashCode = hashCode * 59 + Heading.GetHashCode();
                    if (Stub != null)
                    hashCode = hashCode * 59 + Stub.GetHashCode();
                    if (Matrix != null)
                    hashCode = hashCode * 59 + Matrix.GetHashCode();
                    if (SubjectCode != null)
                    hashCode = hashCode * 59 + SubjectCode.GetHashCode();
                    if (SubjectArea != null)
                    hashCode = hashCode * 59 + SubjectArea.GetHashCode();
                    if (NextUpdate != null)
                    hashCode = hashCode * 59 + NextUpdate.GetHashCode();
                    if (Survey != null)
                    hashCode = hashCode * 59 + Survey.GetHashCode();
                    if (Link != null)
                    hashCode = hashCode * 59 + Link.GetHashCode();
                    if (UpdateFrequency != null)
                    hashCode = hashCode * 59 + UpdateFrequency.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(ExtensionRootPx left, ExtensionRootPx right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(ExtensionRootPx left, ExtensionRootPx right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
