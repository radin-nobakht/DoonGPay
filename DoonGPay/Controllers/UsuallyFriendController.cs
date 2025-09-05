using AutoMapper;
using DoonGPay.Dto;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity;
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
        [HttpPost]
        public IActionResult GetAndConvertUsuallyFriendDtoTotravelFriendDto(int Id)
        {
            var FareeModel = travelService.UsuallyFriends();

            var model = mapper.Map<TravelFriendDto>(FareeModel);

            return PartialView("_EditTravelFriend",model);

            
        }
        [HttpPost]
        public IActionResult UsuallyFriendsModal()
        {
            List<UsuallyFriendDto> model = travelService.UsuallyFriends();
            return PartialView("_EditUsuallyFriend", model);
        }
        [HttpPost]
        public IActionResult UsaullyFrined(int usaullyFrinedId)
        {
            return View(travelService.UsuallyFriend(usaullyFrinedId));
        }
        [HttpGet]
        public IActionResult EditUsuallyFriend(int? Id)
        {
            UsuallyFriendDto fellowDto;
            if (Id.HasValue && Id.Value > 0)
            {
                fellowDto = travelService.UsuallyFriend(Id.Value);
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

            return PartialView("_EditUsuallyFriend", fellowDto);
        }


        [HttpPost]
        public IActionResult SaveUsuallyFriend(UsuallyFriendDto model)
        {
            travelService.SaveUsualyFriend(model);


            return Json(new
            {
                success = true,
                message = travelService.UsuallyFriends()

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