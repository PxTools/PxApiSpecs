namespace PxApi2Dummy.Data
{
    public class ConfigInfo
    {
        /// <summary>
        /// The version of the specification this api conforms to(?).
        /// </summary>
        public string apiVersion { get; set; }

        /// <summary>
        /// List of languages that data can be fetched as
        /// </summary>
        public Language[] languages { get; set; }

        /// <summary>
        /// Which of the langue that are the default language  (Will there be calls to the api without langauge in the url?) 
        /// </summary>
        public string defaultLanguage { get; set; }

        /// <summary>
        /// Comment on DataRetrieval, this trick doesnt seem to work ?
        /// </summary>
        public DataRetrieval dataRetrieval { get; set; }

        /// <summary>
        /// Does this api have more that one source.
        /// </summary>
        public bool isMultiSource { get; set; }






    }
}
