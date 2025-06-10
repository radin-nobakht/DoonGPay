using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Service;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class PayController : Controller
    {
        private readonly ITravelService _travelService;
        public PayController(ITravelService travelService)
        {
            _travelService = travelService;
        }
       
        public IActionResult Index()
        {
            ViewBag.TravelList = _travelService.ShowTravel();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditUser(int? id)
        {
            if (id == null)
                return Json(new TravelEntity());

            var entity = _travelService.GetById(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

        [HttpPost]
        public IActionResult AddOrEditUser(TravelEntity model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "مدل نامعتبر است" });

            if (model.Id == 0)
                _travelService.AddTravel(model);
            else
                _travelService.UpdateTravel(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _travelService.DeleteTravel(id);
            return Json(new { success = true });
        }

    }
}
