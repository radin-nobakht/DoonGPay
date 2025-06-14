using DoonGPay.Dto;

namespace DoonGPay.Inteface
{
    public interface IAccountService
    {
        IBaseResult LoginOtp(LoginDto login);
        IBaseResult LoginSendCode(string phoneNumber);
    }
}
