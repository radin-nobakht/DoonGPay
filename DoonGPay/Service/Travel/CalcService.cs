using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity.Travel;
using DoonGPay.Inteface.Travel;
using System.Data.Common;

namespace DoonGPay.Service.Travel
{
    public class CalcService(MyContext db, IMapper mapper) : ICalcService
    {
        public int Persons(int travelId)
        {
            var persons = db.TravelFriends.Sum(x => x.Person);
            return persons;
        }
        public List<TravelFriendDto> Friends(int travelId)
        {
            var Frined = db.TravelFriends.Where(x => x.TravelId == travelId).Select(x => new { x.Id, x.Person }).ToList();
            return mapper.Map<List<TravelFriendDto>>(Frined);
        }
        public decimal CostValue(int id)
        {
            var costValue = db.TravelCosts
                  .Where(x => x.Id == id)
                  .Select(x => x.Value)
                  .FirstOrDefault();
            return costValue;
        }
        public List<TravelCostFriendDto> GetTravelFriendCost(int travelCostId)
        {
            var CostFriend = db.TravelCostFriends.Where(x => x.TravelCostId == travelCostId).ToList();
            return mapper.Map<List<TravelCostFriendDto>>(CostFriend);
        }
       
        public void SaveTravelCostsFriend(TravelCostFriendDto travelCostFriend)
        {
            var travelCostFriendEntity = mapper.Map<TravelCostFriendEntity>(travelCostFriend);
            if (travelCostFriend.Id == 0) 
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
