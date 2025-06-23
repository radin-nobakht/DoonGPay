using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Service;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class TravelController(ITravelService travelService) : Controller
    {


        //Travel
        public IActionResult Index()
        {
           ViewBag.travelList= travelService.Travels();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditTravel(int? id)
        {
            if (id == null)
                return Json(new TravelDto());

            var entity = travelService.GetByIdTravel(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

        [HttpPost]
        public IActionResult AddOrEditTravel(TravelDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "مدل نامعتبر است" });

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
        //Cost





        public IActionResult Cost()
        {
            ViewBag.costList= travelService.Costs();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditCost(int? id)
        {
            if (id == null)
                return Json(new TravelCostDto());

            var entity = travelService.GetByIdCost(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

        [HttpPost]
        public IActionResult AddOrEditCost(TravelCostDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "مدل نامعتبر است" });

            if (model.Id == 0)
                travelService.AddCost(model);
            else
                travelService.UpdateCost(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteCost(int id)
        {
            travelService.DeleteCost(id);
            return Json(new { success = true });
        }


        //Fellowtraveler


        public IActionResult Fellowtraveler()
        {
            ViewBag.FellowtravelerList = travelService.Fellowtravelers();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditFellowtraveler(int? id)
        {
            if (id == null)
                return Json(new TravelFellowtravelerDto());

            var entity = travelService.GetByIdFellowtraveler(id.Value);
            if (entity == null)
                return Json(new { success = false, message = "کاربر یافت نشد" });

            return Json(entity);
        }

        [HttpPost]
        public IActionResult AddOrEditFellowtraveler(TravelFellowtravelerDto model)
        {
            if (!ModelState.IsValid)
                return Json(new { success = false, message = "مدل نامعتبر است" });

            if (model.Id == 0)
                travelService.AddFellowtraveler(model);
            else
                travelService.UpdateFellowtraveler(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteFellowtraveler(int id)
        {
            travelService.DeleteFellowtraveler(id);
            return Json(new { success = true });
        }
    }
}
