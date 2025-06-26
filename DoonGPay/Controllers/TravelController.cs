using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.Inteface;
using DoonGPay.INteface;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class TravelController(ITravelService travelService) : Controller
    {



        #region Travel
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Travels()
        {
            return PartialView("_Travels", travelService.Travels());
        }
        [HttpPost]
        public IActionResult EditTravel(int? id)
        {
            TravelDto travelDto = new() { Date = DateTime.Now };

            if (id > 0)
                travelDto = travelService.Travel(id.Value);
            return PartialView("_EditTravel", travelDto);
        }

        [HttpPost]
        public IActionResult SaveTravel(TravelDto model)
        {


            if (model.Id == 0)
                travelService.Addtravel(model);
            else
                travelService.Updatetravel(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteTravel(int id)
        {
            travelService.Deletetravel(id);
            return Json(new { success = true });
        }
        #endregion


        #region Cost

        public IActionResult TravelCosts(int travelId)
        {
            ViewData["travelId"] = travelId;
            return PartialView("_TravelCost", travelService.TravelCosts(travelId));
        }

        [HttpPost]
        public IActionResult EditTravelCost(int? travelCostId,int travelId)
        {
            TravelCostDto CostDto = new() { TravelId=travelId};

            if (travelCostId > 0)
                CostDto = travelService.GetByIdCost(travelCostId.Value);
            return PartialView("_EditTravelCost", CostDto);
        }

        [HttpPost]
        public IActionResult SaveCost(TravelCostDto model)
        {
            if (model.Id == 0)
                travelService.AddCost(model);
            else
                travelService.UpdateCost(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteTravelCost(int id)
        {
            travelService.DeleteCost(id);
            return Json(new { success = true });
        }

        #endregion



        #region Friend

        public IActionResult Friend(int travelId)
        {
            ViewData["travelId"] = travelId;
            return PartialView("_TravelFriend", travelService.Friends(travelId));
        }

        [HttpPost]
        public IActionResult EditFriend(int? travelFriendId, int travelId)
        {
            TravelFriendDto fellowDto = new() { TravelId = travelId };

            if (travelFriendId > 0)
                fellowDto = travelService.Friend(travelFriendId.Value);
            return PartialView("_EditFriend", fellowDto);
        }
        [HttpPost]
        public IActionResult SaveFriend(TravelFriendDto model)
        {
            if (model.Id == 0)
                travelService.AddFriend(model);
            else
                travelService.UpdateFriend(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteFriend(int id)
        {
            travelService.DeleteFriend(id);
            return Json(new { success = true });
        }

        #endregion
    }
}
