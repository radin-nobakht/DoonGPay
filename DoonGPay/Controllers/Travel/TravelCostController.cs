using DoonGPay.Dto.Travel;
using DoonGPay.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController : Controller
    {


        public IActionResult TravelCosts(int travelId)
        {
            ViewData["travelId"] = travelId;
            return PartialView("_TravelCost", travelService.TravelCosts(travelId));
        }
        [HttpPost]
        public IActionResult EditTravelCost(int? travelCostId, int travelId)
        {
            var model = new EditCostViewModel
            {
                TravelCost = travelService.TravelCost(travelCostId, travelId),
                TravelFriend=travelService.TravelFriends(travelId),
            };

            return PartialView("_EditTravelCost",model);
        }
        //[HttpPost]
        //public IActionResult SelectTable(int travelId,int CostId)
        //{
        //    var model = new EditCostViewModel();
        //   model.TravelFriend = travelService.TravelFriends(travelId);
        //   model.TravelCostFriend = travelService.TravelCostFriends(CostId);
        //    return PartialView("_EditTravelCost",model);
        //}

        [HttpPost]
        public IActionResult SaveCost(TravelCostDto model)
        {
            travelService.SaveTravelCost(model);


            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteTravelCost(int id)
        {
            travelService.DeleteTravelCost(id);
            return Json(new { success = true });
        }


    }
}
