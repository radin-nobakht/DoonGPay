using DoonGPay.Dto.Travel;
using DoonGPay.Inteface;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController: Controller
    {
      

        [HttpGet]
        public IActionResult EditFriend(int? travelFriendId, int travelId)
        {
            TravelFriendDto fellowDto;

            if (travelFriendId.HasValue && travelFriendId.Value > 0)
            {
                fellowDto = travelService.TravelFriend(travelFriendId.Value);
                if (fellowDto == null)
                {
                    // می‌تونی اینجا خطا یا View خاصی بازگردونی
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
        public IActionResult UsaullyFrineds()
        {
            return View(travelService.UsuallyFriends);
        }
        public IActionResult UsaullyFrined(int usaullyFrinedId)
        {
            return View(travelService.UsuallyFriend(usaullyFrinedId));
        }
    }
}
