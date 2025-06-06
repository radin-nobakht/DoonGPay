using Microsoft.AspNetCore.Mvc;
using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Models;
using System.Diagnostics;


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
        public override int GetHashCode()
        {
            return EqualityComparer<IUserService>.Default.GetHashCode(_userService);
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
                    return Redirect("Index");
                }
            }
            ViewBag.eror = "هم رمز کسی وجود ندارد";
            return View();
        }
        [HttpGet]
        [Route("Signin")]
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signin(UsersEntity entity)
        {

            _userService.AddUser(entity);
            var redirectUrl = Url.Action("Index", "Home");
            return Json(new
            {
                success = true,
                redirectUrl,
            });
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserMangment()
        {
            ViewBag.UserList = _userService.ShowUsers();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditUser(int? id)
        {
            if (id == null)
                return View(new UsersEntity());

            var entity = _userService.GetById(id.Value);
            if (entity == null)
                return NotFound();

            return View(entity);
        }

        [HttpPost]
        public IActionResult AddOrEditUser(UsersEntity model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "مدل نامعتبر است" });

            if (model.Id == 0)
                _userService.AddUser(model);
            else
                _userService.UpdateUser(model);
            ViewBag.UserList = _userService.ShowUsers();

            var redirectUrl = Url.Action("UserMangment", "Home");
            return RedirectToAction("UserMangment");
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _userService.DeleteUser(id);
            return Json(new { success = true });
        }

        public override bool Equals(object? obj)
        {
            return obj is HomeController controller &&
                   EqualityComparer<IUserService>.Default.Equals(_userService, controller._userService);
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
