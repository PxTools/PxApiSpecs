namespace PxApi2Dummy.Models
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

        /// <summary>
        /// For menuItemType "table"
        /// </summary>
        public DateTime? updated { get; set; }

        public Link[] links { get; set; }
        public MenuItem[] children { get; set; }

    }
}
