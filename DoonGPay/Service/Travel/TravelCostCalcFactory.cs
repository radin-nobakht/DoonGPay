using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity.Travel;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoonGPay.Service.Travel
{
    public static class TravelCostCalcFactory
    {
        public static ITravelCostCalc Create(int value, MyContext db)
        {
            return value switch
            {
                1 => new TravelCostCalcEqual(db),
                2 => new TravelCostCalcPerson(db),
                3 => new TravelCostCalcPersonManual(db),
                4 => new TravelCostCalcValue(db),
                5 => new TravelCostCalcPercent(db),
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
    public class TravelCostCalcPerson(MyContext db) : ITravelCostCalc
    {
         public IBaseResult Validate (TravelCostDto travelCost)
        {
            return new BaseResult(true);
        }
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
    public class TravelCostCalcEqual(MyContext db) : ITravelCostCalc
    {
        public IBaseResult Validate(TravelCostDto travelCost)
        {
            return new BaseResult(true);
        }
        public TravelCostDto Calc(TravelCostDto travelCost)
        {

            var friend = db.TravelFriends.Where(x => x.TravelId == travelCost.TravelId).Select(x => new { x.Id, x.Person }).ToList();
            var share = travelCost.Value / friend.Count;

             foreach (var tcf in travelCost.TravelCostFriends)
                tcf.Value = share;

            return travelCost;

        }
    }
    public class TravelCostCalcPercent(MyContext db) : ITravelCostCalc
    {
        public IBaseResult Validate(TravelCostDto travelCost)
        {
           if(travelCost.TravelCostFriends.Sum(x=>x.Rate) != 100)
            {

                return new BaseResult(false, "جمع درصد های شما 100 نمی شود");

            }
            return new BaseResult(true);
        }
        public TravelCostDto Calc(TravelCostDto travelCost)
        {
            var share = travelCost.Value / 100;
            foreach (var tcf in travelCost.TravelCostFriends)
                tcf.Value = tcf.Rate * share;
            return travelCost;


        }
    }
    public class TravelCostCalcPersonManual(MyContext db) : ITravelCostCalc
    {
        public IBaseResult Validate(TravelCostDto travelCost)
        {
            return new BaseResult(true);
        }

        public TravelCostDto Calc(TravelCostDto travelCost)
        {

            var share = travelCost.Value / travelCost.TravelCostFriends.Sum(x => x.Rate);

            var finishList = new TravelCostFriendDto();

            foreach (var tcf in travelCost.TravelCostFriends)
                tcf.Value =tcf.Rate * share;


            return travelCost;
        }
    }
    public class TravelCostCalcValue(MyContext db) : ITravelCostCalc
    {
        public IBaseResult Validate(TravelCostDto travelCost)
        {
            if (travelCost.TravelCostFriends.Sum(x => x.Rate) != travelCost.Value)
            {
              
                return new BaseResult(false, $"جمع هزینه های شما با {travelCost.Value} مساوی نیست لطفا اصلاح کنید");
            }
            return new BaseResult(true);
        }

        public TravelCostDto Calc(TravelCostDto travelCost)
        {
            foreach (var tcf in travelCost.TravelCostFriends)
                tcf.Value = tcf.Rate;

            return travelCost;
        }
    }
}

