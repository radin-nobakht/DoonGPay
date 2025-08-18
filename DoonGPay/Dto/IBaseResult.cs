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
        public BaseResult(bool result) { Result = result; }
        public BaseResult(bool result, string message) { Result = result; Message = message; }
        public BaseResult(bool result, string message, object data) { Result = result; Message = message; Data = data; }
        public BaseResult(string message) { Result = false; Message = message; }
        public bool Result { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }
    }
}
