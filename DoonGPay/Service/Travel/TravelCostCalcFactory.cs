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
        public static List<TravelCostType> Types()
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
        public static List<SelectListItem> SelectTypes()=> [.. Types().Select(x=> new SelectListItem (x.Title,x.Value.ToString()))];

    }

    public class TravelCostType(string type, string title, int value)
    {
        public string Type { get; set; } = type;
        public string Title { get; set; } = title;
        public int Value { get; set; } = value;
    }

    public class TravelCostCalcPerson : ITravelCostCalc
    {
        public void Calc(int travelCostId)
        {

        }
    }
    public class TravelCostCalcEqual : ITravelCostCalc
    {
        public void Calc(int travelCostId)
        {

        }
    }

    public class TravelCostCalcEqual2: ITravelCostCalc
    {
        public void Calc(int travelCostId)
        {

        }
    }
    public class TravelCostCalcPercent : ITravelCostCalc
    {
        public void Calc(int travelCostId)
        {

        }
    }
    public class TravelCostCalcPersonManual : ITravelCostCalc
    {
        public void Calc(int travelCostId)
        {

        }
    }
    public class TravelCostCalcValue : ITravelCostCalc
    {
        public void Calc(int travelCostId)
        {

        }
    }


}
