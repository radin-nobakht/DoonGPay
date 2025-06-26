using DoonGPay.Dto.Travel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController : Controller
    {
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
    }
}
