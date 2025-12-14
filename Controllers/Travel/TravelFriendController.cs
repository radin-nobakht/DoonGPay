using DoonGPay.Dto.Travel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController : Controller
    {
        [HttpGet]
        public IActionResult TravelCostFriend(int travelFriendId, int travelId)
        {
            var model = travelService.TravelCostFriend(travelId, travelFriendId);
            return PartialView("_TravelCostFriendInfo", model);
        }

        [HttpGet]
        public IActionResult EditFriend(int? travelFriendId, int travelId)
        {
            TravelFriendDto fellowDto;

            if (travelFriendId.HasValue && travelFriendId.Value > 0)
            {
                fellowDto = travelService.TravelFriend(travelFriendId.Value);
                if (fellowDto == null)
                {
                    return NotFound();
                }
            }
            else
            {
                fellowDto = new TravelFriendDto { TravelId = travelId };
            }

            return PartialView("_EditTravelFriend", fellowDto);
        }

        [HttpPost]
        public JsonResult SaveFriend(TravelFriendDto model)
        {
            try
            {
                travelService.SaveTravelFriend(model);
                return Json(new { success = true, message = "ذخیره شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // حذف دوست
        [HttpPost]
        public JsonResult DeleteFriend(int id)
        {
            try
            {
                travelService.DeleteTravelFriend(id);
                return Json(new { success = true, message = "حذف شد" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
