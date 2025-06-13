using Microsoft.AspNetCore.Mvc;
using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Models;
using System.Diagnostics;
using DoonGPay.Dto;

namespace DoonGPay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UsersEntity entity)
        {
            var model = _userService.Users();
            foreach (var i in model)
            {
                if (i.Name == entity.Name && i.LName == entity.LName && i.PhoneNumber == entity.PhoneNumber)
                {
                    return RedirectToAction("UserMangment", "Home");
                }
            }
            ViewBag.eror = "کاربری با این مشخصات یافت نشد.";
            return View();
        }

        [HttpGet]
        [Route("Signin")]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signin(UserDto user)
        {
            _userService.AddUser(user);
            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("UserMangment", "Home")
            });
        }

        public IActionResult UserMangment()
        {
            ViewBag.UserList = _userService.Users();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditUser(int? id)
        {
            if (id == null)
                return Json(new travels());

            var entity = _userService.GetById(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

        [HttpPost]
        public IActionResult AddOrEditUser(UserDto user)
        {

            if (!ModelState.IsValid)
                return Json(new { success = false, message = "مدل نامعتبر است" });

            if (user.Id == 0)
                _userService.AddUser(user);
            else
                _userService.UpdateUser(user);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return Json(new { success = true });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
