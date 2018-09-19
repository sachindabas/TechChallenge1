namespace CurrencyToWord.Entities
{
    public class Response<T> : BaseResponse
    {
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
