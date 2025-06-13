using DoonGPay.Dto;
using DoonGPay.Inteface;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class AccountController(IAccountService accountService) : Controller
    {
        [Route("Login")]
        [HttpGet]
        public IActionResult Login() => View();
        [HttpPost]
        public JsonResult Login(LoginDto login) => Json(accountService.Login(login));


    }
}
