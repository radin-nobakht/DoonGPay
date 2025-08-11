using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity.Travel;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoonGPay.Service.Travel
{
    public static class TravelCostCalcFactory
    {
        public static ITravelCostCalc Create(int value , MyContext db, IMapper mapper)
        {
            return value switch
            {
                1 => new TravelCostCalcEqual(db,mapper),
                2 => new TravelCostCalcPerson(db, mapper),
                3 => new TravelCostCalcPersonManual(db, mapper),
                4 => new TravelCostCalcValue(db, mapper),
                5 => new TravelCostCalcPercent(db, mapper),
                _ => throw new NotImplementedException("خطا در انجام عملیات"),
            };
        }
        public static ICollection<TravelCostType> Types()
        {
            return
            [
                new("TravelCostCalcEqual","به صورت مساوی",1),
                new("TravelCostCalcPerson","بر حسب نفرات",2),
                new("TravelCostCalcPersonManual","بر حسب نفرات دستی",3),
                new("TravelCostCalcValue","دستی",4),
                new("TravelCostCalcPercent","درصد",5)
            ];
        }
        public static List<SelectListItem> SelectTypes() => [.. Types().Select(x => new SelectListItem(x.Title, x.Value.ToString()))];

    }

    public class TravelCostType(string type, string title, int value)
    {
        public string Type { get; set; } = type;
        public string Title { get; set; } = title;
        public int Value { get; set; } = value;
    }

    public class TravelCostCalcPerson(MyContext db, IMapper mapper) : ITravelCostCalc
    {

       
        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {
            // اگر travelId یک مقدار ساده است (مثلاً int)
            var persons = db.TravelFriends
                             .Where(x => x.TravelId == travelId)
                             .Sum(x => x.Person);
            var costValue = db.TravelCosts
                             .Where(x => x.Id == travelCostId)
                             .Select(x => x.Value)
                             .FirstOrDefault();
            var share = travelCostId / persons;
            var Friends = db.TravelFriends.Where(x => x.TravelId == travelId).Select(x => new { x.Id, x.Person }).ToList();
            var finishList = new TravelCostFriendDto();
            if (add = true)
            {
                foreach (var f in Friends)
                {

                    finishList.Value = f.Person * share;
                    finishList.TravelFriendId = f.Id;
                    finishList.TravelCostId = travelCostId;
                    var travelCostFriendEntity = mapper.Map<TravelCostFriendEntity>(Friends);
                    db.TravelCostFriends.Add(travelCostFriendEntity);
                }
            }
            else
            {
                var CostFriend = db.TravelCostFriends.Where(x => x.TravelCostId == travelCostId).ToList();
                foreach (var fcl in CostFriend)
                {
                    finishList.Id = fcl.Id;
                    foreach (var f in Friends)
                    {
                        if (fcl.TravelFriendId == f.Id)
                        {
                            finishList.Value = f.Person * share;
                        }
                    }
                    var travelCostFriendEntity = mapper.Map<TravelCostFriendEntity>(finishList);
                    db.TravelCostFriends.Update(travelCostFriendEntity);
                }
                db.SaveChanges();

            }
        }
    }
    public class TravelCostCalcEqual(MyContext db,IMapper mapper) : ITravelCostCalc
    {

      

        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {
            var costValue = db.TravelCosts
                             .Where(x => x.Id == travelCostId)
                             .Select(x => x.Value)
                             .FirstOrDefault();
             var frined = db.TravelFriends.Where(x => x.TravelId == travelId).Select(x => new { x.Id, x.Person }).ToList(); 
            var share = costValue / frined.Count;
            var finishList = new TravelCostFriendDto();
            if (add)
            {
                foreach (var f in frined)
                {
                    finishList.Value = share;
                    finishList.TravelFriendId = f.Id;
                    finishList.TravelCostId = travelCostId;
                    var travelCostFriendEntity = mapper.Map<TravelCostFriendEntity>(finishList);
                    db.TravelCostFriends.Add(travelCostFriendEntity);
                }
            }
            else
            {
                var travelCostFriend = db.TravelCostFriends.Where(x => x.TravelCostId == travelCostId).ToList();
                foreach (var fcl in travelCostFriend)
                {
                    finishList.Value = share;
                    finishList.Id = fcl.Id;

                    var travelCostFriendEntity = mapper.Map<TravelCostFriendEntity>(finishList);
                    db.TravelCostFriends.Update(travelCostFriendEntity);
                }
            }
            db.SaveChanges();

        }
    }
    public class TravelCostCalcPercent(MyContext db, IMapper mapper) : ITravelCostCalc
    {
        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {

        }
    }
    public class TravelCostCalcPersonManual(MyContext db, IMapper mapper) : ITravelCostCalc
    {

        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {

        }
    }
    public class TravelCostCalcValue(MyContext db, IMapper mapper) : ITravelCostCalc
    {
       
        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {
            var list = new TravelCostFriendDto();
            foreach (var fcl in travelCostFriends)
            {
                list.Value = fcl.Value;
                list.Id = fcl.Id;
                list.TravelFriendId = fcl.TravelFriendId;
                list.TravelCostId = travelCostId;

                var travelCostFriendEntity = mapper.Map<TravelCostFriendEntity>(list);
                if (travelCostFriendEntity.Id == 0)
                {
                    db.TravelCostFriends.Add(travelCostFriendEntity);

                }
                else
                {
                    db.TravelCostFriends.Update(travelCostFriendEntity);

                }
                    db.SaveChanges();

            }
        }
    }


}
