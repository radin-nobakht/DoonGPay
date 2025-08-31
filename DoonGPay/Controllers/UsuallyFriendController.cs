using DoonGPay.Dto;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class UsuallyFriendController(ITravelService travelService, IMySession mySession) : Controller
    {
        public IActionResult Index()
        {
            var model = travelService.UsuallyFriends();
            return View(model);
        }
        [HttpPost]
        public IActionResult UsaullyFrined(int usaullyFrinedId)
        {
            return View(travelService.UsuallyFriend(usaullyFrinedId));
        }
        [HttpPost]
        public IActionResult EditUsuallyFriend(int? usaullyFrinedId)
        {
            UsuallyFriendDto fellowDto;
            if (usaullyFrinedId.HasValue && usaullyFrinedId.Value > 0)
            {
                fellowDto = travelService.UsuallyFriend(usaullyFrinedId.Value);
                if (fellowDto == null)
                {
                    // می‌تونی اینجا خطا یا View خاصی بازگردونی
                    return NotFound();
                }
            }
            else
            {
                var userId = mySession.UserId;
                fellowDto = new UsuallyFriendDto { UserId = userId };
            }

            return PartialView("_EditUsaullyFriend", fellowDto);
        }


        [HttpPost]
        public IActionResult SaveUsuallyFriend(UsuallyFriendDto model)
        {
            travelService.SaveUsualyFriend(model);


            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteUsuallyFriend(int id)
        {
            travelService.DeleteUsuallyFriend(id);
            return Json(new { success = true });
        }
    }
}