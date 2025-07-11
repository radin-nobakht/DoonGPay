using AutoMapper;
using DoonGPay.Inteface.Travel;
using DoonGPay.Adapter;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity.Travel;
using DoonGPay.Inteface;
using DoonGPay.Inteface.Travel;
using DoonGPay.INteface;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.CodeDom;
using System.Linq;


namespace DoonGPay.Service.Travel
{
   public class TravelService(MyContext db, IMapper mapper, IMySession mySession) : ITravelService
    {
        #region TravelCostFriend

        public void SaveTravelCostFriend(TravelCostFriendDto travelCostFriend)
        {
            var model = mapper.Map<TravelCostFriendEntity>(travelCostFriend);
            if(model.Id>0)
                db.TravelCostFriends.Update(model);
            else
                db.TravelCostFriends.Add(model);
            db.SaveChanges();
        }
        public List<TravelCostFriendDto> TravelCostFriends(int travelCostId)
        {
            var data = db.TravelCostFriends.Where(x => x.TravelCostId == travelCostId).ToList();

            return mapper.Map<List<TravelCostFriendDto>>(data);
        }
        public void DeleteTravelCostFriend(int id)
        {
            var model = db.TravelCostFriends.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.TravelCostFriends.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion

        #region Travel

        public List<TravelDto> AllTravel(int travelId)
        {
            var data = db.Travels.Where(x => x.Id == travelId)
                .Include(x => x.TravelCosts.Where(x => x.TravelId == travelId))
                .Include(x => x.TravelFriends.Where(x => x.TravelId == travelId))
                .ToList();

            return mapper.Map<List<TravelDto>>(data);
        }


        public void SaveTravel(TravelDto travel)
        {
            var model = mapper.Map<TravelEntity>(travel);
            model.InsertDate = DateTime.Now;
            model.UserId = (int)mySession.UserId;
            if (model.Id > 0)
                db.Travels.Update(model);
            else
                db.Travels.Add(model);
            db.SaveChanges();
        }
        public List<TravelDto> Travels()
        {
            var data = db.Travels.Where(x => x.UserId == mySession.UserId).ToList();

            return mapper.Map<List<TravelDto>>(data);
        }
        public void DeleteTravel(int id)
        {
            var model = db.Travels.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.Travels.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelDto Travel(int id)
        {
            return mapper.Map<TravelDto>(db.Travels.FirstOrDefault(x => x.Id == id));
        }
        #endregion

        #region Friend
        public void SaveTravelFriend(TravelFriendDto travelFriends)
        {
            var model = mapper.Map<TravelFriendEntity>(travelFriends);

            if (model.Id > 0)
                db.TravelFriends.Update(model);
            else
                db.TravelFriends.Add(model);
            db.SaveChanges();
        }
        //public List<TravelFriendDto> Friends_ShareByRow(int travelId)
        //{

        //    var data = db.TravelFriends.Where(x => x.TravelId == travelId).ToList();
        //    var model = mapper.Map<List<TravelFriendDto>>(data);
        //    if (model.Count > 0)
        //    {
        //        var costs = db.TravelCosts.Where(x => x.TravelId == travelId).ToList();
        //        var costmodel = mapper.Map<List<TravelCostDto>>(costs);

        //        // جمع تمام مقادیر هزینه‌ها
        //        var totalValue = costmodel.Sum(x => x.Value);

        //        // محاسبه سهم هر فرد
        //        var share = totalValue / model.Count;

        //        foreach (var friend in model)
        //        {
        //            friend.Share = share;
        //        }
        //    }


        //    return model;
        //}
        //public List<TravelFriendDto> Friends_ShareByPerson(int travelId)
        //{

        //    var data = db.TravelFriends.Where(x => x.TravelId == travelId).ToList();
        //    var model = mapper.Map<List<TravelFriendDto>>(data);
        //    var countPerson = model.Sum(x => x.Person);

        //    if (model.Count > 0)
        //    {
        //        var totalValue = db.TravelCosts.Where(x => x.TravelId == travelId).Sum(x => x.Value);


        //        // محاسبه سهم هر فرد
        //        decimal onePersonShare = (decimal)totalValue / (decimal)countPerson;

        //        foreach (var friend in model)
        //        {
        //            decimal share = onePersonShare * friend.Person;
        //            friend.Share = share;
        //        }
        //    }


        //    return model;
        //}
        public List<TravelFriendDto> TravelFriends(int travelId)
        {
            var data = db.TravelFriends.Where(x => x.TravelId == travelId).ToList();
            return mapper.Map<List<TravelFriendDto>>(data); 
        }


        public TravelFriendDto TravelFriend(int FriendId)
        {

            var data = db.TravelFriends.FirstOrDefault(x => x.Id == FriendId);

            return mapper.Map<TravelFriendDto>(data);
        }
       
        public void DeleteTravelFriend(int id)
        {
            var model = db.TravelFriends.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.TravelFriends.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion

        #region Cost
        public void SaveTravelCost(TravelCostDto travelCost)
        {
            var model = mapper.Map<TravelCostEntity>(travelCost);
            if (model.Id > 0)
                db.TravelCosts.Update(model);
            else
                db.TravelCosts.Add(model);
            db.SaveChanges();
            
            var travelCostCalc= TravelCostCalcFactory.Create(travelCost.Type);
            travelCostCalc.Calc(model.Id);


        }
        public List<TravelCostDto> TravelCosts(int travelId)
        {

            var data = db.TravelCosts.Where(x => x.TravelId == travelId).ToList();
            var list = mapper.Map<List<TravelCostDto>>(data);
            var costTypes = TravelCostTypes();

            foreach (var tc in list)
            {
                var type = costTypes.FirstOrDefault(x => x.Value == tc.Type.ToString());
                tc.TypeStr = type != null ? type.Text : "نامشخص"; // یا مقدار پیش‌فرض دلخواه
            }




            return list;
        }
        private List<SelectListItem> TravelCostTypes() => TravelCostCalcFactory.SelectTypes();
        public TravelCostDto TravelCost(int? travelCostId, int travelId)
        {

            TravelCostDto costDto = new() { TravelId = travelId };

            if (travelCostId > 0)
            {
                var cost = db.TravelCosts
                    .Include(x => x.TravelCostFriends.Where(z => z.TravelCostId == travelCostId))
                    .FirstOrDefault(x => x.Id == travelCostId);

                costDto = mapper.Map<TravelCostDto>(cost);

            }
            costDto.CostTypes = TravelCostTypes();

            return costDto;
        }
      
        public void DeleteTravelCost(int id)
        {
            var model = db.TravelCosts.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.TravelCosts.Remove(model);
                db.SaveChanges();
            }
        }
        #endregion


    }
}
