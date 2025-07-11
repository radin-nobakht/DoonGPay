using DoonGPay.Dto.Travel;
using DoonGPay.Inteface;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController: Controller
    {
        public IActionResult Friend(int travelId)
        {
            ViewData["travelId"] = travelId;
            return PartialView("_TravelFriend", travelService.TravelFriends(travelId));
        }

        [HttpPost]
        public IActionResult EditFriend(int? travelFriendId, int travelId)
        {
            TravelFriendDto fellowDto = new() { TravelId = travelId };

            if (travelFriendId > 0)
                fellowDto = travelService.TravelFriend(travelFriendId.Value);
            return PartialView("_EditFriend", fellowDto);
        }
        [HttpPost]
        public IActionResult SaveFriend(TravelFriendDto model)
        {
            travelService.SaveTravelFriend(model);


            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteFriend(int id)
        {
            travelService.DeleteTravelFriend(id);
            return Json(new { success = true });
        }
    }
}
