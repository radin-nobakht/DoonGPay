namespace DoonGPay.Dto
{
    public interface IBaseResult
    {
        bool Result { get; set; }
        string Message { get; set; }
        Object Data { get; set; }
    }
    public class BaseResult : IBaseResult
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
