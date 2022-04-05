namespace PxApi2Dummy.Models
{
    public class SearchResultItem:MenuItem
    {

        /// <summary>
        /// Id of source application instance where the table is found.
        /// </summary>
        public string sourceId { get; set; }

        /// <summary>
        /// Search score (relevance of search)
        /// </summary>
        public float score { get; set; }



        public SearchResultItem() 
        {
        }

        public SearchResultItem(Source source, ApiV1.SearchResultItem oldItem, string lang)
        {
            this.sourceId = source.Id;

            menuItemType = MenuItemType.TABLE;

            id = oldItem.id;
            label = oldItem.title;
            updated = oldItem.published;
            score = oldItem.score;

            string tableUrl = String.Format(source.SearchToTableFormat[lang], oldItem.mypath, oldItem.id);
            
            links = new Link[1];
            Link tmp = new Link { href = tableUrl, rel = "metadata" };
            links[0] = tmp;

        }
    }
}