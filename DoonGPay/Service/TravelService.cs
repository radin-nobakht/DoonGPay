using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.INteface;


namespace DoonGPay.Service
{
    public class TravelService(MyContext db, IMapper mapper) : ITravelService
    {
        public void Addtravel(TravelDto travel)
        {
            travel.InsertDate = DateTime.Now;

            var model = mapper.Map<TravelEntity>(travel);
            db.Travels.Add(model);
            db.SaveChanges();
        }
        public List<TravelDto> Travels()
        {

            var data = db.Travels.ToList();

            return mapper.Map<List<TravelDto>>(data);
        }
        public void Updatetravel(TravelDto travel)
        {
            var model = mapper.Map<TravelEntity>(travel);
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
        public TravelDto GetByIdTravel(int id)
        {
            return mapper.Map<TravelDto>(db.Travels.FirstOrDefault(x => x.Id == id));
        }
        public void AddFellowtraveler(TravelFellowtravelerDto travelFellowtravelers)
        {
            var model = mapper.Map<TravelFellowtravelerEntity>(travelFellowtravelers);

            db.TravelFellowtravelers.Add(model);
            db.SaveChanges();
        }
        public List<TravelFellowtravelerDto> Fellowtravelers()
        {

            var data = db.TravelFellowtravelers.ToList();

            return mapper.Map<List<TravelFellowtravelerDto>>(data);
        }
        public void UpdateFellowtraveler(TravelFellowtravelerDto travelFellowtravelers)
        {
            var model = mapper.Map<TravelFellowtravelerEntity>(travelFellowtravelers);
            db.TravelFellowtravelers.Update(model);
            db.SaveChanges();
        }
        public void DeleteFellowtraveler(int id)
        {
            var model = db.TravelFellowtravelers.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.TravelFellowtravelers.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelFellowtravelerDto GetByIdFellowtraveler(int id)
        {
            return mapper.Map<TravelFellowtravelerDto>(db.TravelFellowtravelers.FirstOrDefault(x => x.Id == id));
        }
        public void AddCost(TravelCostDto travelCost)
        {
            var model = mapper.Map<TravelCostEntity>(travelCost);

            db.TravelCosts.Add(model);
            db.SaveChanges();
        }
        public List<TravelCostDto> Costs()
        {

            var data = db.TravelCosts.ToList();

            return mapper.Map<List<TravelCostDto>>(data);
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



    }
}
