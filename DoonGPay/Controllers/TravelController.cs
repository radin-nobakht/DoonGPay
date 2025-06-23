using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.INteface;
using DoonGPay.Service;
using Microsoft.AspNetCore.Mvc;

namespace DoonGPay.Controllers
{
    public class travelController : Controller
    {
        private readonly ItravelService _travelService;
        public travelController(ItravelService travelService)
        {
            _travelService = travelService;
        }
        //Travel
        public IActionResult Index()
        {
           ViewBag.travelList= _travelService.travels();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditTravel(int? id)
        {
            if (id == null)
                return Json(new TravelDto());

            var entity = _travelService.GetByIdTravel(id.Value);
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
                _travelService.Addtravel(model);
            else
                _travelService.Updatetravel(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteTravel(int id)
        {
            _travelService.Deletetravel(id);
            return Json(new { success = true });
        }
        //Cost





        public IActionResult Cost()
        {
            ViewBag.costList= _travelService.Costs();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditCost(int? id)
        {
            if (id == null)
                return Json(new TravelCostDto());

            var entity = _travelService.GetByIdCost(id.Value);
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
                _travelService.AddCost(model);
            else
                _travelService.UpdateCost(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteCost(int id)
        {
            _travelService.DeleteCost(id);
            return Json(new { success = true });
        }


        //Fellowtraveler


        public IActionResult Fellowtraveler()
        {
            ViewBag.FellowtravelerList = _travelService.Fellowtravelers();
            return View();
        }

        [HttpGet]
        public IActionResult AddOrEditFellowtraveler(int? id)
        {
            if (id == null)
                return Json(new TravelFellowtravelerDto());

            var entity = _travelService.GetByIdFellowtraveler(id.Value);
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
                _travelService.AddFellowtraveler(model);
            else
                _travelService.UpdateFellowtraveler(model);

            return Json(new
            {
                success = true,
                message = "ذخیره شد"
            });
        }

        [HttpPost]
        public JsonResult DeleteFellowtraveler(int id)
        {
            _travelService.DeleteFellowtraveler(id);
            return Json(new { success = true });
        }
    }
}
