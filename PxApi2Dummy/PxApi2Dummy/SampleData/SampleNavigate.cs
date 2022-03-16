using PxApi2Dummy.Data;

namespace PxApi2Dummy.SampleData
{
    public class SampleNavigate
    {
        public static List<MenuItem> Get()
        {
            string hrefFormat = "http://my-site.com/api/v2/navigate/{0}";
            List<MenuItem> myOut = new List<MenuItem>();
            MenuItem temp = new MenuItem();
            temp.id = "BE0101";
            temp.menuItemType = "Level";
            temp.label = "Befolkningsstatistik";
            List<Link> tempLinks = new List<Link>();
            tempLinks.Add(new Link() { rel = "data", href = String.Format(hrefFormat, temp.id) });
            temp.links = tempLinks.ToArray();
           
            myOut.Add(temp);

            return myOut;

        } 

    }
}
