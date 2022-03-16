namespace PxApi2Dummy.Data
{
    public class MenuItem
    {

        public string id { get; set; }
        
        public string label { get; set; }

        public Link[] links { get; set; }

        public int sortOrder { get; set; }

        private string _menuItemType;
        public string menuItemType
        { 
            get { return _menuItemType; }
            set { _menuItemType = MenuItemType.Force(value); } 
        }



    }
}
