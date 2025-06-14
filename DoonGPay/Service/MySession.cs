using DoonGPay.Inteface;
using System.Security.Claims;

namespace DoonGPay.Service
{
    public class MySession(IHttpContextAccessor httpContextAccessor) : IMySession
    {
        public int? UserId => int.TryParse(httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int id) ? id : null;

        public string? FirstName
        {
            get
            {
                return httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "FirstName")?.Value;
            }
        }

        public string? LastName => httpContextAccessor?.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "LastName")?.Value;

    }


}
