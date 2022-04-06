namespace PxApi2Dummy.Models.Helpers
{
    public class Paginated<T> : Response<T>
    {

        public int offset { get; set; }
        public int size { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public Paginated(T Data, int offset, int size)
        {
            this.offset = offset;
            this.size = size;
            this.Data = Data;
        }
    }
}

