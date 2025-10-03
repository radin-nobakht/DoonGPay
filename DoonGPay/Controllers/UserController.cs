using Microsoft.AspNetCore.Mvc;
using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Models;
using System.Diagnostics;
using DoonGPay.Dto;

namespace DoonGPay.Controllers
{
    public class UserController(IUserService userService, IWebHostEnvironment env) : Controller
    {
        
        public async Task<IActionResult> ImageSaving(IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                string uploadFolder = Path.Combine(env.WebRootPath, "Image");

                if (!Directory.Exists(uploadFolder))
                    Directory.CreateDirectory(uploadFolder);

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // اینجا فقط آدرس نسبی فایل رو می‌گیری
                string relativePath = "~/Image/" + fileName;

                // می‌تونی تو ViewBag برگردونی
                ViewBag.FileUrl = relativePath;

                // یا مثلا به صورت JSON برگردونی
                return Json(new { url = relativePath });
            }

            return BadRequest("هیچ فایلی انتخاب نشده است");
        }

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
            var model = userService.Users();
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

            userService.AddUser(user);
            
            return Json(new
            {
                success = true,
                redirectUrl = Url.Action("Index", "Home")
            });
        }

        public IActionResult UserMangment()
        {
       
            return View(userService.Users());
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
