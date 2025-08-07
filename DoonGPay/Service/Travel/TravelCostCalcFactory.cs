using DoonGPay.Adapter;
using DoonGPay.Dto.Travel;
using DoonGPay.Inteface.Travel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DoonGPay.Service.Travel
{
    public static class TravelCostCalcFactory
    {
        public static ITravelCostCalc Create(int value)
        {
            return value switch
            {
                1 => new TravelCostCalcEqual(),
                2 => new TravelCostCalcPerson(),
                3 => new TravelCostCalcPersonManual(),
                4 => new TravelCostCalcValue(),
                5 => new TravelCostCalcPercent(),
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

    public class TravelCostCalcPerson() : ITravelCostCalc
    {
        private readonly ICalcService _calcService;
        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {
            var persons = _calcService.Persons(travelId);
            var costs = _calcService.CostValue(travelCostId);
            var share = costs / persons;
            var friendsList = _calcService.Friends(travelId);
            var finishList = new TravelCostFriendDto();
            if (add = true)
            {
                friendsList = _calcService.Friends(travelId);
                foreach (var f in friendsList)
                {

                    finishList.Value = f.Person * share;
                    finishList.TravelFriendId = f.Id;
                    finishList.TravelCostId = travelCostId;

                    _calcService.SaveTravelCostsFriend(finishList);
                }
            }
            else
            {
                var friendCostsList = _calcService.GetTravelFriendCost(travelId);
                foreach (var fcl in friendCostsList)
                {
                    finishList.Id = fcl.Id;
                    foreach (var f in friendsList)
                    {
                        if (fcl.TravelFriendId == f.Id)
                        {
                            finishList.Value = f.Person * share;
                        }
                    }
                    _calcService.SaveTravelCostsFriend(finishList);
                }


            }
        }
    }
    public class TravelCostCalcEqual() : ITravelCostCalc
    {
        private readonly ICalcService _calcService;
        public void Calc(int travelCostId, int travelId, bool add, ICollection <TravelCostFriendDto> travelCostFriends)
        {
            var costs = _calcService.CostValue(travelCostId);
            var friendsList = _calcService.Friends(travelId);
            var share = costs / friendsList.Count;
            var finishList = new TravelCostFriendDto();
            if (add)
            {
                foreach (var f in friendsList)
                {
                    finishList.Value = share;
                    finishList.TravelFriendId = f.Id;
                    finishList.TravelCostId = travelCostId;

                    _calcService.SaveTravelCostsFriend(finishList);
                }
            }
            else
            {
                var friendCostsList = _calcService.GetTravelFriendCost(travelId);
                foreach (var fcl in friendCostsList)
                {
                    finishList.Value = share;
                    finishList.Id = fcl.Id;
                    _calcService.SaveTravelCostsFriend(finishList);
                }
            }
        }
    }
    public class TravelCostCalcPercent() : ITravelCostCalc
    {
        private readonly ICalcService _calcService;
        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {

        }
    }
    public class TravelCostCalcPersonManual() : ITravelCostCalc
    {
        private readonly ICalcService _calcService;

        public void Calc(int travelCostId, int travelId, bool add, ICollection <TravelCostFriendDto> travelCostFriends)
        {

        }
    }
    public class TravelCostCalcValue : ITravelCostCalc
    {
        private readonly ICalcService _calcService;
        public void Calc(int travelCostId, int travelId, bool add, ICollection<TravelCostFriendDto> travelCostFriends)
        {
            var list = new TravelCostFriendDto();
         foreach(var fcl in travelCostFriends)
            {
              list.Value = fcl.Value;
                list.Id = fcl.Id;
                list.TravelFriendId = fcl.TravelFriendId;
                list.TravelCostId = travelCostId;
                _calcService.SaveTravelCostsFriend(list);
            }         
        }
    }


}
