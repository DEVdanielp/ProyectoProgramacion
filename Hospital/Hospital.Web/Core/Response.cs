namespace Hospital.Web.Core
{
    public class Response<T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<String> Errors { get; set; }
        public T Result { get; set; }
    }
}
