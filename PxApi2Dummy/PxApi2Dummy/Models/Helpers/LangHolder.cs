namespace PxApi2Dummy.Models
{
    public class LangHolder
    {
        /// <summary>
    /// The lang in url, if any
    /// </summary>
        public string? UrlLang { get; set; }

        private string defaultLang="no";

        public LangHolder(string? lang)
        {
            this.UrlLang = lang;
        }

        //private readonly string dataLang;

        /// <summary>
        /// UrlLang if present, otherWise the defaultLang
        /// </summary>
        public string GetDataLang()
        {
            return UrlLang == null? defaultLang : UrlLang;
        }
    }
}