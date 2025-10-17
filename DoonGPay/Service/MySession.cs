using DoonGPay.Adapter;
using DoonGPay.Inteface;
using System.Security.Claims;

namespace DoonGPay.Service
{
    public class MySession(IHttpContextAccessor httpContextAccessor, MyContext db) : IMySession
    {
        public int? UserId => int.TryParse(httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int id) ? id : null;
        public string? ImageStr()
        {
            var imageStr = db.Users
                .Where(x => x.Id == UserId)
                .Select(x => x.UserAvatarStr)
                .FirstOrDefault();
            return imageStr;

        }
        public bool IsLogin => UserId > 0;
        public string? FullName => IsLogin ? $"{FirstName} {LastName} ({UserName})" : null;
        public string? FirstName
        {
            get
            {
                //to do
                return httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "FirstName")?.Value;
            }
        }

        public string? LastName => httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "LastName")?.Value;

        public string? UserName => httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "UserName")?.Value;

    }


}
