using DoonGPay.Dto.Travel;
using PersianDate;
using DoonGPay.Entity;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using DoonGPay.INteface;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController(ITravelService travelService) : Controller
    {
        public IActionResult Index()
        {
           TravelDto model =travelService.Travel(0);
            ViewData["travelId"] = model.Id;
            //var travels=travelService.Travels();
            //travels.First.TravelFriends = travelService.TravelFriends(travels.First.Id);
            //travels.First.TravelCosts = travelService.TravelCosts(travels.First.Id);
            return View(model);
        }
        public IActionResult ChangeTravel(int id)
        {
            var model = travelService.Travel(id);
            
            return PartialView("_Travel",model);
        }
        [HttpPost]
        //public IActionResult Travels()
        //{
        //    return PartialView("_Travels", travelService.Travel);
        //}
        public IActionResult Travels()
        {
            List<TravelDto> model = travelService.Travels();
            return PartialView("_Travels",model);
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


            travelService.SaveTravel(model);


            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteTravel(int id)
        {
            travelService.DeleteTravel(id);
            return Json(new { success = true });
        }


   
    }
}
