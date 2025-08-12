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
        public static ITravelCostCalc Create(int value, MyContext db, IMapper mapper)
        {
            return value switch
            {
                1 => new TravelCostCalcEqual(db, mapper),
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


        public TravelCostDto Calc(TravelCostDto travelCost)
        {
            // اگر travelId یک مقدار ساده است (مثلاً int)
            var persons = db.TravelFriends
                             .Where(x => x.TravelId == travelCost.TravelId)
                             .Sum(x => x.Person);
           
            var share = travelCost.Id / persons;
            var friends = db.TravelFriends.Where(x => x.TravelId == travelCost.TravelId).Select(x => new { x.Id, x.Person }).ToList();
            var finishList = new TravelCostFriendDto();
            if (travelCost.Id > 0)
            {
                foreach (var f in friends)
                {

                    finishList.Value = f.Person * share;
                    finishList.TravelFriendId = f.Id;
                    finishList.TravelCostId = travelCost.Id;
                   
                }
            }
            else
            {
                var CostFriend = db.TravelCostFriends.Where(x => x.TravelCostId == travelCost.Id).ToList();
                foreach (var fcl in CostFriend)
                {
                    finishList.Id = fcl.Id;
                    foreach (var f in friends)
                    {
                        if (fcl.TravelFriendId == f.Id)
                        {
                            finishList.Value = f.Person * share;
                        }
                    }
                    var travelCostFriendEntity = mapper.Map<TravelCostFriendEntity>(finishList);
                    db.TravelCostFriends.Update(travelCostFriendEntity);
                }

            }
            return travelCost;

        }
    }
    public class TravelCostCalcEqual(MyContext db, IMapper mapper) : ITravelCostCalc
    {

        public TravelCostDto Calc(TravelCostDto travelCost)

        {

            var friend = db.TravelFriends.Where(x => x.TravelId == travelCost.TravelId).Select(x => new { x.Id, x.Person }).ToList();
            var share = travelCost.Value / friend.Count;
            var finishList = new List<TravelCostFriendDto>();
            if (travelCost.Id > 0)
            {
                foreach (var f in friend)
                {
                    finishList.Add(new TravelCostFriendDto
                    {
                        Value = share,
                        TravelFriendId = f.Id,
                        TravelCostId = travelCost.Id
                    });
                }
            }
            else
            {
                var travelCostFriend = db.TravelCostFriends.Where(x => x.TravelCostId == travelCost.Id).ToList();
                foreach (var fcl in travelCostFriend)
                {
                    decimal value = 0;
                    foreach (var f in friend)
                    {
                        if (fcl.TravelFriendId == f.Id)
                        {
                           value = f.Person * share;
                        }
                    }
                    travelCost.TravelCostFriends.Add(new TravelCostFriendDto
                    {
                        Id = fcl.Id,
                        Value= value
                    });
                }
            }
            return travelCost;

        }
    }
    public class TravelCostCalcPercent(MyContext db, IMapper mapper) : ITravelCostCalc
    {
        public TravelCostDto Calc(TravelCostDto travelCost)
        {
            return travelCost;

        }
    }
    public class TravelCostCalcPersonManual(MyContext db, IMapper mapper) : ITravelCostCalc
    {

        public TravelCostDto Calc(TravelCostDto travelCost)
        {
            return travelCost;
        }
    }
    public class TravelCostCalcValue(MyContext db, IMapper mapper) : ITravelCostCalc
    {

        public TravelCostDto Calc(TravelCostDto travelCost)
        {
            foreach (var fcl in travelCost.TravelCostFriends)
            {
                travelCost.TravelCostFriends.Add(new TravelCostFriendDto
                {
                    Value = fcl.Value,
                    Id = fcl.Id,
                    TravelFriendId = fcl.TravelFriendId,
                    TravelCostId = travelCost.Id
                });
            }
            return travelCost;
        }
    }
}




