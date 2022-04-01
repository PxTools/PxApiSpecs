namespace PxApi2Dummy.Models
{
    public class MenuItemType
    {
        static List<string> items = new List<string>() {"root", "folder-expanded", "folder-closed", "table","heading" };
        public static string ROOT = "root";

        public static string FOLDER_EXPANDED = "folder-expanded";
        public static string FOLDER_CLOSED = "folder-closed";
        public static string TABLE = "table";
        public static string HEADING = "heading";

        public static string Force(string val)
        {
            if (!items.Contains(val))
            {
                String temp = String.Join(',', items.ToArray());
                throw new Exception(String.Format("Invalid value for MenuItemType:\"{0}\". Valid values are: {1}.", val, temp));
            }
            return val;
            
        }
    }
}