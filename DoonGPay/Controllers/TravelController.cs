using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Service;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class travelController : Controller
    {
        private readonly ItravelService _travelService;
        public travelController(ItravelService travelService)
        {
            _travelService = travelService;
        }
       
        public IActionResult Index()
        {
            ViewBag.travelList = _travelService.travels();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditUser(int? id)
        {
            if (id == null)
                return Json(new travels());

            var entity = _travelService.GetById(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

        [HttpPost]
        public IActionResult AddOrEditUser(travels model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "مدل نامعتبر است" });

            if (model.Id == 0)
                _travelService.Addtravel(model);
            else
                _travelService.Updatetravel(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _travelService.Deletetravel(id);
            return Json(new { success = true });
        }

    }
}
