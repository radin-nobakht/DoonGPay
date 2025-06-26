using DoonGPay.Dto.Travel;
using DoonGPay.Entity;
using DoonGPay.Inteface.Travel;
using DoonGPay.INteface;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers.Travel
{
    public partial class TravelController(ITravelService travelService, IFriendTravelService friendTravelService, ICostTravelService costTravelService) : Controller
    {



        #region Travel
        public IActionResult Index()
        {
            return View(travelService.Travels());
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


   



        #region Friend



        #endregion
    }
}
