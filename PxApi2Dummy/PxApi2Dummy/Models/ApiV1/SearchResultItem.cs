using System.Runtime.Serialization;

namespace PxApi2Dummy.Models.ApiV1
{
    /// <summary>
    /// Represents a table found by a search
    /// </summary>
    public class SearchResultItem
    {
        /// <summary>
        /// Id of table
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Table path within its database
        /// </summary>
        [DataMember(Name = "path")]
        public string mypath { get; set; }
        
        /// <summary>
        /// Table title
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Search score (relevance of search)
        /// </summary>
        public float score { get; set; }

        /// <summary>
        /// The date the table was published
        /// </summary>
        public DateTime published { get; set; }
    }


}