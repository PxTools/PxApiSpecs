using PxApi2Dummy.Data;

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
            myOut.languages = languages.ToArray();

            myOut.defaultLanguage = "sv";

            myOut.dataRetrieval = new DataRetrieval();
            myOut.dataRetrieval.maxDataCells = 12345;


            return myOut;

        } 

    }
}
