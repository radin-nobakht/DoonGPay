using DoonGPay.Dto;

namespace DoonGPay.Inteface
{
    public interface IAccountService
    {
        IBaseResult Login(LoginDto login);
    }
}
