using AutoMapper;
using DoonGPay.Dto;
using DoonGPay.Dto.Travel;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class UsuallyFriendController(ITravelService travelService, IMySession mySession, IMapper mapper) : Controller
    {
        public IActionResult Index()
        {
            var model = travelService.UsuallyFriends();
            return View(model);
        }

        // 📌 نمایش لیست دوستان معمولی داخل مودال
        [HttpPost]
        public IActionResult UsuallyFriendsModal()
        {
            var model = travelService.UsuallyFriends();
            return PartialView("_UsuallyFriendsModal", model);
        }

        // 📌 انتخاب یک دوست و تبدیل آن به TravelFriendDto
        [HttpPost]
        public IActionResult GetAndConvertUsuallyFriendDtoTotravelFriendDto(int id)
        {
            var friend = travelService.UsuallyFriend(id);
            if (friend == null) return NotFound();

            var model = mapper.Map<TravelFriendDto>(friend);
            return PartialView("_EditTravelFriend", model);
        }

        // 📌 ادیت یک UsuallyFriend
        [HttpGet]
        public IActionResult EditUsuallyFriend(int? id)
        {
            UsuallyFriendDto fellowDto;
            if (id.HasValue && id.Value > 0)
            {
                fellowDto = travelService.UsuallyFriend(id.Value);
                if (fellowDto == null)
                {
                    return NotFound();
                }
            }
            else
            {
                var userId = mySession.UserId;
                fellowDto = new UsuallyFriendDto { UserId = userId };
            }

            return PartialView("_EditUsuallyFriend", fellowDto);
        }

        // 📌 ذخیره دوست معمولی
        [HttpPost]
        public IActionResult SaveUsuallyFriend(UsuallyFriendDto model)
        {
            travelService.SaveUsualyFriend(model);

            return Json(new
            {
                success = true,
                message = "اطلاعات با موفقیت ذخیره شد"
            });
        }

        // 📌 حذف دوست معمولی
        [HttpPost]
        public JsonResult DeleteUsuallyFriend(int id)
        {
            travelService.DeleteUsuallyFriend(id);
            return Json(new { success = true });
        }
    }
}
