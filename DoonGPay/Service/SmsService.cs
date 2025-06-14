using DoonGPay.Dto;
using DoonGPay.Inteface;

namespace DoonGPay.Service
{
    public class SmsService:ISmsService
    {
        public IBaseResult SendSms(string phoneNumber, SmsType smsType)
        {
            return new BaseResult { Result = true };
        }

        public IBaseResult ValidateSms(string phoneNumber, string code, SmsType smsType)
        {
            if (code == "1234")
                return new BaseResult { Result = true };
            return new BaseResult { Result = false ,Message="کد دریافتی اشتباه می باشد" };
        }
    }
}
