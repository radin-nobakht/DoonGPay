using Microsoft.AspNetCore.Mvc;
using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Models;
using System.Diagnostics;
using DoonGPay.Dto;

namespace DoonGPay.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController( IUserService userService)
        {
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
            var model = _userService.ShowUsers();
            foreach (var i in model)
            {
                if (i.Name == entity.Name && i.LName == entity.LName && i.PhoneNumber == entity.PhoneNumber)
                {
                    return RedirectToAction("UserMangment", "User");
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
                redirectUrl = Url.Action("UserMangment", "User")
            });
        }

        public IActionResult UserMangment()
        {
       
            return View(_userService.ShowUsers());
        }

        [HttpGet]
        public IActionResult AddOrEditUser(int? id)
        {
            if (id == null)
                return Json(new UsersEntity());

            var entity = _userService.GetById(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

      

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return Json(new { success = true });
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
