using DoonGPay.Dto.Travel;
using DoonGPay.Helpers;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController(ITravelService travelService,IMySession mySession): Controller
    {
     

        // صفحه اصلی سفر
        public IActionResult Index(int id)
        {
            var cookieValue = ConvertIdToCookie.GetCookie(Request);

            if (cookieValue != null) 
            {
               id = int.Parse(cookieValue);
            }

            TravelDto model = travelService.Travel(id);
            ViewData["travelId"] = model.Id;
           var userId= mySession.UserId;
            if (userId > 0)
                return View(model);
            else
                return RedirectToAction("Login","Account");
        }

        public IActionResult LoadTravels(int id)
        {
            var cookieValue = ConvertIdToCookie.GetCookie(Request);

            if (cookieValue != null)
            {
                id = int.Parse(cookieValue);
            }

            TravelDto model = travelService.Travel(id);
            ViewData["travelId"] = model.Id;
            var userId = mySession.UserId;
            if (userId > 0)
                return PartialView("Index",model);
            else
                return RedirectToAction("Login", "Account");
        }

        // تغییر سفر فعال
        public IActionResult ChangeTravel(int id)
        {
            var model = travelService.Travel(id);

            ConvertIdToCookie.AddCookie(Response, id.ToString(), 7);

            return PartialView("_Travel", model);
        }

        // لیست تمام سفرها
        public IActionResult Travels()
        {
            List<TravelDto> model = travelService.Travels();
            return PartialView("_Travels", model);
        }

        // فرم Add/Edit سفر
        [HttpGet]
        public IActionResult EditTravel(int? id)
        {
            TravelDto travelDto = new();
            if (id.HasValue && id.Value > 0)
                travelDto = travelService.Travel(id.Value);

            return PartialView("_EditTravel", travelDto);
        }

        // ذخیره سفر
        [HttpPost]
        public JsonResult SaveTravel(TravelDto model)
        {
            try
            {
                travelService.SaveTravel(model);
                return Json(new { success = true, message = "ذخیره شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // حذف سفر
        [HttpPost]
        public JsonResult DeleteTravel(int id)
        {
            try
            {
                travelService.DeleteTravel(id);
                return Json(new { success = true, message = "حذف شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
