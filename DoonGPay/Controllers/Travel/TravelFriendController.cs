using DoonGPay.Dto.Travel;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController: Controller
    {
        public IActionResult Friend(int travelId)
        {
            ViewData["travelId"] = travelId;
            return PartialView("_TravelFriend", friendTravelService.FriendsbyPerson(travelId));
        }

        [HttpPost]
        public IActionResult EditFriend(int? travelFriendId, int travelId)
        {
            TravelFriendDto fellowDto = new() { TravelId = travelId };

            if (travelFriendId > 0)
                fellowDto = friendTravelService.Friend(travelFriendId.Value);
            return PartialView("_EditFriend", fellowDto);
        }
        [HttpPost]
        public IActionResult SaveFriend(TravelFriendDto model)
        {
            if (model.Id == 0)
                friendTravelService.AddFriend(model);
            else
                friendTravelService.UpdateFriend(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteFriend(int id)
        {
            friendTravelService.DeleteFriend(id);
            return Json(new { success = true });
        }
    }
}
