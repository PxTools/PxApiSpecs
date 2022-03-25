namespace Api1Client
{
    using System.Text.Json;
    using Utf8Json;

    public class ApiV1Client
    {
        public Dictionary<string, ApiV1MenuItem[]> Properties { get; set; }
        private string startUrl;
        private HttpClient _httpClient;

        private int itemCnt = 0;

        public ApiV1Client()
        {
            ItemLists temp = new ItemLists();
            string jsonString = File.ReadAllText("from_www.qa.ssb.no.txt");
            temp = Utf8Json.JsonSerializer.Deserialize<ItemLists>(jsonString);
            Properties = temp.Properties;
        }

        


        private void DumpApiToFile()
        {
            startUrl = "https://data.qa.ssbX.no/api/v0/no/table/";
            _httpClient = new HttpClient();
            /* in this would normally be a service, but since this it a one off ...
             * from a blazor app:
                     public TableListService(HttpClient _http)
        {
            // @inject (or the [Inject] attribute) isn't available for use in services. 
            _httpClient = _http;
        }
             */

            ReadMenuFromApi("");

            ItemLists temp = new ItemLists();
            temp.Properties = Properties;

            var options = new JsonSerializerOptions { WriteIndented = true };
            string myOut = System.Text.Json.JsonSerializer.Serialize(temp, options);

            File.WriteAllTextAsync("WriteText.txt", myOut).Wait();
        }




        private void ReadMenuFromApi(string path)
        {
            /*itemCnt++;
            if(itemCnt > 5)
            {
                return;
            }*/
            var menuItems = getMenuItemFromApi(path);
            Properties.Add(path, menuItems);
           
            foreach(ApiV1MenuItem item in menuItems)
            {
                if (item.type == "l")
                {
                    ReadMenuFromApi(path + "/" + item.id);
                }
            }

        }

        private ApiV1MenuItem[] getMenuItemFromApi(string path)
        {
            string url = startUrl + path;
            System.Threading.Thread.Sleep(2000);
            string jsonString = _httpClient.GetStringAsync(url).Result;
            ApiV1MenuItem[] myOut = Utf8Json.JsonSerializer.Deserialize<ApiV1MenuItem[]>(jsonString);
            return myOut;
        }

       
    }
}