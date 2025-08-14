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


    //بر حسب نفرات ثبت شده
    public class TravelCostCalcPerson(MyContext db, IMapper mapper) : ITravelCostCalc
    {
   
        public TravelCostDto Calc(TravelCostDto travelCost)
        {

            var travelFriends = db.TravelFriends.Where(x => x.TravelId == travelCost.TravelId).Select(x => new { x.Person, x.Id }).ToList();

            var share = travelCost.Value / travelFriends.Sum(x => x.Person);

            var finishList = new TravelCostFriendDto();

            foreach (var tcf in travelCost.TravelCostFriends)
                tcf.Value = travelFriends.First(x => x.Id == tcf.TravelFriendId).Person * share;


            return travelCost;
        }

    }
    public class TravelCostCalcEqual(MyContext db, IMapper mapper) : ITravelCostCalc
    {

        public TravelCostDto Calc(TravelCostDto travelCost)
        {

            var friend = db.TravelFriends.Where(x => x.TravelId == travelCost.TravelId).Select(x => new { x.Id, x.Person }).ToList();
            var share = travelCost.Value / friend.Count;

             foreach (var tcf in travelCost.TravelCostFriends)
                tcf.Value = share;

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
            travelCost.Type = 2;
            var travelCostCalc = TravelCostCalcFactory.Create(travelCost.Type, db, mapper);
             travelCost = travelCostCalc.Calc(travelCost);
            travelCost.Type = 3;
            return travelCost;
        }
    }
    public class TravelCostCalcValue(MyContext db, IMapper mapper) : ITravelCostCalc
    {

        public TravelCostDto Calc(TravelCostDto travelCost)
        {
            foreach (var tcf in travelCost.TravelCostFriends)
                tcf.Value = travelCost.TravelCostFriends.First(x => x.Id == tcf.TravelFriendId).Value ;

            return travelCost;
        }
    }
}
