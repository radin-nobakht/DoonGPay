using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.Inteface;
using DoonGPay.INteface;


namespace DoonGPay.Service
{
    public class TravelService(MyContext db, IMapper mapper, IMySession mySession) :ITravelService
, ITravelService
    {
        #region Travel
        public void Addtravel(TravelDto travel)
        {
            var model = mapper.Map<TravelEntity>(travel);
            model.InsertDate = DateTime.Now;
            model.UserId = (int)mySession.UserId;
            db.Travels.Add(model);
            db.SaveChanges();
        }
        public List<TravelDto> Travels()
        {
            var data = db.Travels.Where(x => x.UserId == mySession.UserId).ToList();

            return mapper.Map<List<TravelDto>>(data);
        }
        public void Updatetravel(TravelDto travel)
        {

            var model = mapper.Map<TravelEntity>(travel);
            model.InsertDate = DateTime.Now;
            db.Travels.Update(model);
            db.SaveChanges();
        }
        public void Deletetravel(int id)
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
        public void AddFriend(TravelFriendDto travelFriends)
        {
            var model = mapper.Map<TravelFriendEntity>(travelFriends);

            db.TravelFriends.Add(model);
            db.SaveChanges();
        }
        public List<TravelFriendDto> Friends(int travelId)
        {

            var data = db.TravelFriends.Where(x => x.TravelId == travelId).ToList();
            return mapper.Map<List<TravelFriendDto>>(data);
        }

        public TravelFriendDto Friend(int FriendId)
        {

            var data = db.TravelFriends.Where(x => x.Id == FriendId).ToList();

            return mapper.Map<TravelFriendDto>(data);
        }
        public void UpdateFriend(TravelFriendDto travelFriends)
        {
            var model = mapper.Map<TravelFriendEntity>(travelFriends);
            db.TravelFriends.Update(model);
            db.SaveChanges();
        }
        public void DeleteFriend(int id)
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
        public void AddCost(TravelCostDto travelCost)
        {
            var model = mapper.Map<TravelCostEntity>(travelCost);

            db.TravelCosts.Add(model);
            db.SaveChanges();
        }
        public List<TravelCostDto> TravelCosts(int travelId)
        {

            var data = db.TravelCosts.Where(x => x.TravelId == travelId).ToList();
            return mapper.Map<List<TravelCostDto>>(data);
        }

        public TravelCostDto TravelCost(int travelCostId)
        {

            var data = db.TravelCosts.Where(x => x.Id == travelCostId).ToList();

            return mapper.Map<TravelCostDto>(data);
        }
        public void UpdateCost(TravelCostDto travelCost)
        {
            var model = mapper.Map<TravelCostEntity>(travelCost);
            db.TravelCosts.Update(model);
            db.SaveChanges();
        }
        public void DeleteCost(int id)
        {
            var model = db.TravelCosts.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.TravelCosts.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelCostDto GetByIdCost(int id)
        {
            return mapper.Map<TravelCostDto>(db.TravelCosts.FirstOrDefault(x => x.Id == id));
        }
        #endregion


    }
}
