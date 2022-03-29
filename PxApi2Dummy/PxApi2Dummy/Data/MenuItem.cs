namespace PxApi2Dummy.Data
{
    public class MenuItem
    {

        public string id { get; set; }

        private string _menuItemType;
        public string menuItemType
        {
            get { return _menuItemType; }
            set { _menuItemType = MenuItemType.Force(value); }
        }

        public string label { get; set; }
        public int sortOrder { get; set; }

        public Link[] links { get; set; }
        public MenuItem[] children { get; set; }

    }
}
