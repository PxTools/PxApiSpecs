namespace PxApi2Dummy.Data
{
    public class MenuItemType
    {
        static List<string> items = new List<string>() {"Level","Table","Heading" };


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