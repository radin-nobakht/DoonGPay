using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Inteface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace DoonGPay.Service
{
    public class AccountService(MyContext db, ISmsService smsService, IHttpContextAccessor httpContextAccessor) : IAccountService
    {
        public IBaseResult LoginOtp(LoginDto login)
        {
            var user = db.Users.FirstOrDefault(x => x.PhoneNumber == login.PhoneNumber);

            if (user == null)
                return new BaseResult
                {
                    Result = false,
                    Message = "کاربر پیدا نشد"
                };

            var result = smsService.ValidateSms(login.PhoneNumber, login.Code, SmsType.Login);
            if (!result.Result) { return result; }


            var claims = new List<Claim>
            {
             new (ClaimTypes.NameIdentifier, user.Id.ToString()),
             new ("LastName", user.LastName),
             new ("FirstName", user.FirstName)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                //IsPersistent = login.Remember,
                IsPersistent=true,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            };
            httpContextAccessor.HttpContext?.SignInAsync(principal, properties);
            return new BaseResult
            {
                Result = true
            };
        }

        public IBaseResult LoginSendCode(string phoneNumber)
        {
            return smsService.SendSms(phoneNumber, SmsType.Login);
        }
    }
}
