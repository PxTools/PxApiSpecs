namespace PxApi2Dummy.Models
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
        public string Error { get; set; }
        public string Info { get; set; }
    }
}
