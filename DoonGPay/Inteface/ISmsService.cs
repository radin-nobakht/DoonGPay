using DoonGPay.Dto;

namespace DoonGPay.Inteface
{
    public interface ISmsService
    {
        IBaseResult SendSms(string phoneNumber, SmsType smsType);
        IBaseResult ValidateSms(string phoneNumber, string code, SmsType smsType);
    }

    public enum SmsType
    {
        Login,
        ForgetPassword,
    }
}

