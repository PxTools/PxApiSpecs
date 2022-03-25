using PxApi2Dummy.Data;

namespace PxApi2Dummy.SampleData
{
    public class SampleNavigate
    {
        private static Dictionary<string, MenuItem> data;

        public static void InitSampleNavigate()
        {
            Api1Client.ApiV1Client tmp = new Api1Client.ApiV1Client();
            var v1Menu = tmp.Properties;
            data = new Dictionary<string, MenuItem>();
            var rootOld = v1Menu[""];


            var root= new MenuItem();
            root.id = "";
            root.menuItemType = MenuItemType.ROOT;

            List<MenuItem> kids = new List<MenuItem>();
            foreach (var item in rootOld)
            {
                if (item.type == "l")
                {
                    MenuItem temp = new MenuItem();
                    temp.id = item.id;
                    temp.label = item.text;
                    temp.menuItemType = MenuItemType.FOLDER_CLOSED;
                    kids.Add(temp);
                }
            } 
            root.children = kids.ToArray();

            data.Add("",root);
            

        }
            


        public static MenuItem Get(string urlToController, string id)
        {
            MenuItem root = new MenuItem();
            root.id = "tabl "+id;
            return root;
        }



            public static MenuItem Get(string urlToController)
        {
            MenuItem root = data[""];
            return root;
            
            /*
            int sortOrder = 1;

            string hrefFormat = urlToController+"/{0}";
            string tableHrefFormat = "http://my-site.com/api/v2/tables/{0}";

            List<MenuItem> myOut = new List<MenuItem>();


            temp.sortOrder = sortOrder++;
            List<Link> tempLinks = new List<Link>();
            tempLinks.Add(new Link() { rel = "data", href = String.Format(hrefFormat, temp.id) });
            temp.links = tempLinks.ToArray();
            myOut.Add(temp);

            temp = new MenuItem();
            temp.id = "bef";
            temp.menuItemType = MenuItemType.TABLE;
            temp.label = "Befolkningsframskrivningar";
            temp.sortOrder = sortOrder++;
            tempLinks = new List<Link>();
            tempLinks.Add(new Link() { rel = "data", href = String.Format(hrefFormat, temp.id) });
            temp.links = tempLinks.ToArray();

            myOut.Add(temp);


            temp = new MenuItem();
            temp.id = "TAB0001";
            temp.menuItemType = MenuItemType.TABLE;
            temp.label = "Tabell A";
            temp.sortOrder = sortOrder++;
            tempLinks = new List<Link>();
            tempLinks.Add(new Link() { rel = "metadata", href = String.Format(tableHrefFormat, temp.id) });
            tempLinks.Add(new Link() { rel = "data", href = String.Format(tableHrefFormat, temp.id+"/data") });
            temp.links = tempLinks.ToArray();

            myOut.Add(temp);

            root.children = myOut.ToArray();
            return root;
            */

        } 

    }
}
