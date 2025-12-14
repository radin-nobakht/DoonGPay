using Microsoft.AspNetCore.Mvc;
using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Models;
using System.Diagnostics;
using DoonGPay.Dto;
using DoonGPay.Inteface;

namespace DoonGPay.Controllers
{
    public class UserController(IUserService userService, IWebHostEnvironment env,IImageService imageSaver) : Controller
    {
        
        

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserEntity entity)
        {
            var model = userService.User();
            foreach (var i in model)
            {
                if (i.FristName == entity.FristName && i.LastName == entity.LastName && i.PhoneNumber == entity.PhoneNumber)
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
            user.UserAvatarStr = imageSaver.SaveImage(user.UserAvatar);

            userService.AddUser(user);

            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        public IActionResult UserMangment(int pageNum)
        {
       
            return View(userService.UserList(pageNum, "/User/UserMangment?pageNum="));
        }

        public JsonResult Users()
        {
            return Json(userService.Users());
        }

        [HttpGet]
        public IActionResult AddOrEditUser(int? id)
        {
            if (id == null)
                return Json(new UserDto());

            var entity = userService.GetById(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            userService.DeleteUser(id);
            return Json(new { success = true });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
