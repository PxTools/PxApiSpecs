using PxApi2Dummy.Models;

namespace PxApi2Dummy.SampleData
{
    public class SampleConfigInfo
    {
        public static ConfigInfo Get()
        {
            ConfigInfo myOut = new ConfigInfo();
            myOut.apiVersion = "2.0";
            List<Language> languages = new List<Language>();

            languages.Add(new() { id = "no", label = "Norsk" });
            languages.Add(new() { id = "sv", label = "Svenska" });
            languages.Add(new() { id = "en", label = "English" });
            myOut.languages = languages.ToArray();

            myOut.defaultLanguage = "sv";

            myOut.dataRetrieval = new DataRetrieval();
            myOut.dataRetrieval.maxDataCells = 12345;

            myOut.isMultiSource = true;

            var tempSource = new Source();
            tempSource.Id = "ssb.no";
            tempSource.apiVersjon = "1.0";
            tempSource.Name = new()
            {
                { "no", "SSB" },
                { "en", "Stat Norway" }
            };
            tempSource.SearchUrl = new()
            {
                { "no", "https://data.qa.ssb.no/api/v0/no/table/?query=" },
                { "en", "https://data.qa.ssb.no/api/v0/en/table/?query=" }
            };

            tempSource.SearchToTableFormat = new()
            {
                { "no", "https://data.qa.ssb.no/api/v0/no/table/{1}" },
                { "en", "https://data.qa.ssb.no/api/v0/en/table/{1}" }
            };


            //https://data.qa.ssb.no/api/v0/no/table/?query=*&filter=*

            var tempSource2 = new Source();
            tempSource2.Id = "nav.no";
            tempSource2.apiVersjon = "1.0";
            tempSource2.Name = new()
            {
                { "no", "Nav" },
                { "en", "Nav Norway" }
            };
            tempSource2.SearchUrl = new()
            {
                { "no", "https://www.qa.ssb.no/navbank/api/v0/no/nav/?query=" },
                { "en", "https://www.qa.ssb.no/navbank/api/v0/en/nav/?query=" }
            };

            tempSource2.SearchToTableFormat = new()
            {
                { "no", "https://www.qa.ssb.no/navbank/api/v0/no/nav/{0}/{1}" },
                { "en", "https://www.qa.ssb.no/navbank/api/v0/en/nav/{0}/{1}" }
            };

            myOut.sources = new Source[] { tempSource, tempSource2 };

            return myOut;

        } 

    }
}
