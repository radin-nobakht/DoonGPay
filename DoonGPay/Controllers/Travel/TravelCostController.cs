using DoonGPay.Dto.Travel;
using DoonGPay.Inteface.Travel;
using DoonGPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController : Controller
    {
        // لیست هزینه‌ها برای یک سفر
        public IActionResult TravelCosts(int travelId)
        {
            ViewData["travelId"] = travelId;
            var model = travelService.TravelCosts(travelId);
            return PartialView("_TravelCost", model);
        }

        // فرم Add/Edit هزینه
        [HttpGet]
        public IActionResult EditTravelCost(int? travelCostId, int travelId)
        {
            var model = travelService.TravelCost(travelCostId, travelId);
            return PartialView("_EditTravelCost", model);
        }

        // ذخیره هزینه
        [HttpPost]
        public JsonResult SaveTravelCost(TravelCostDto travelCost)
        {
            try
            {
                travelService.SaveTravelCost(travelCost);
                return Json(new { success = true, message = "ذخیره شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // حذف هزینه
        [HttpPost]
        public JsonResult DeleteTravelCost(int id)
        {
            try
            {
                travelService.DeleteTravelCost(id);
                return Json(new { success = true, message = "حذف شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
