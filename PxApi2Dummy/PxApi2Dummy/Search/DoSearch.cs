using PxApi2Dummy.Models;

namespace PxApi2Dummy.Search
{
    public class DoSearch
    {

        public static SearchResult Get(string searchText, LangHolder myLang)
        {
            SearchResult myOut = new SearchResult();
            myOut.paginationStuff = "do to";
            
            string lang = myLang.GetDataLang();
            HttpClient _httpClient = new HttpClient();

            List<SearchResultItem> tmp = new List<SearchResultItem>();

            ConfigInfo conf = SampleData.SampleConfigInfo.Get();
            foreach (Source s in conf.sources)
            {
                //if s.apiVersjon
                string url = s.SearchUrl[lang]+searchText;
                string jsonString = _httpClient.GetStringAsync(url).Result;
                List<SearchResultItem> fromOneSource =  readJson(jsonString, s, lang);
               tmp.AddRange(fromOneSource);

            }
            myOut.data = tmp.ToArray();
            return myOut;
        }


        private static List<SearchResultItem> readJson(string jsonString, Source source, string lang)
        {
            List<SearchResultItem> myOut = new List<SearchResultItem>();

            var old = Utf8Json.JsonSerializer.Deserialize<Models.ApiV1.SearchResultItem[]>(jsonString);
            foreach (var item in old)
            {   
                myOut.Add(new SearchResultItem(source, item, lang));
            }
            return myOut;
        }

        


    }
}
