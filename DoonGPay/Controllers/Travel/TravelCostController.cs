using DoonGPay.Dto.Travel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController : Controller
    {


        public IActionResult TravelCosts(int travelId)
        {
            ViewData["travelId"] = travelId;
            return PartialView("_TravelCost", costTravelService.TravelCosts(travelId));
        }

        [HttpPost]
        public IActionResult EditTravelCost(int? travelCostId, int travelId)
        {
            return PartialView("_EditTravelCost", costTravelService.TravelCost(travelCostId, travelId));
        }

        [HttpPost]
        public IActionResult SaveCost(TravelCostDto model)
        {
            if (model.Id == 0)
                costTravelService.AddCost(model);
            else
                costTravelService.UpdateCost(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteTravelCost(int id)
        {
            costTravelService.DeleteCost(id);
            return Json(new { success = true });
        }


    }
}
