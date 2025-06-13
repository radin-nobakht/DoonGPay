using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Inteface;

namespace DoonGPay.Service
{
    public class AccountService(MyContext db) : IAccountService
    {
        public IBaseResult Login(LoginDto login)
        {
            var user = db.Users.FirstOrDefault(x => x.PhoneNumber == login.PhoneNumber);
            if (user == null)
                return new BaseResult
                {
                    Result = false,
                    Message = "کاربر پیدا نشد"
                };
            if (login.Code != "1234")
                return new BaseResult
                {
                    Result = false,
                    Message = "کد اشتباه است"
                };

            return new BaseResult { Result = true };
        }
    }
}
