namespace PxApi2Dummy.Models
{
    public class Source
    {
        public string Id { get; set; }
        public string apiVersjon { get; set; }
        public Dictionary<string,string> Name { get; set; }
        public Dictionary<string, string> SearchUrl { get; set; }
        public Dictionary<string, string> SearchToTableFormat { get; set; }


    }
}
