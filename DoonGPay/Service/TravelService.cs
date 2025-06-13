using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.INteface;


namespace DoonGPay.Service
{
    public class travelService(MyContext db,IMapper mapper) : ItravelService
    {
        public void Addtravel(TravelDto travel)
        {
            var model = mapper.Map<TravelEntity>(travel);

            db.travels.Add(model);
            db.SaveChanges();
        }
        public List<TravelDto> travels()
        {

            var data = db.travels.ToList();

            return mapper.Map<List<TravelDto>>(data);
        }
        public void Updatetravel(TravelDto travel)
        {
            var model = mapper.Map<TravelEntity>(travel);
            db.travels.Update(model);
            db.SaveChanges();
        }
        public void Deletetravel(int id)
        {
            var model = db.travels.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.travels.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelDto GetById(int id)
        {
            return mapper.Map<TravelDto>(db.travels.FirstOrDefault(x => x.Id == id));
        }
        public void AddFellowtraveler(TravelFellowtravelerDto travelFellowtravelers)
        {
            var model = mapper.Map<TravelFellowtravelerEntity>(travelFellowtravelers);

            db.travelFellowtravelers.Add(model);
            db.SaveChanges();
        }
        public List<TravelFellowtravelerDto> Fellowtravelers()
        {

            var data = db.travelFellowtravelers.ToList();

            return mapper.Map<List<TravelFellowtravelerDto>>(data);
        }
        public void UpdateFellowtraveler(TravelFellowtravelerDto travelFellowtravelers)
        {
            var model = mapper.Map<TravelFellowtravelerEntity>(travelFellowtravelers);
            db.travelFellowtravelers.Update(model);
            db.SaveChanges();
        }
        public void DeleteFellowtraveler(int id)
        {
            var model = db.travelFellowtravelers.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.travelFellowtravelers.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelFellowtravelerDto GetByIdFellowtraveler(int id)
        {
            return mapper.Map<TravelFellowtravelerDto>(db.travelFellowtravelers.FirstOrDefault(x => x.Id == id));
        }
        public void AddPay(TravelCostDto travelCost)
        {
            var model = mapper.Map<TravelCostEntity>(travelCost);

            db.travelCosts.Add(model);
            db.SaveChanges();
        }
        public List<TravelCostDto> Pays()
        {

            var data = db.travelCosts.ToList();

            return mapper.Map<List<TravelCostDto>>(data);
        }
        public void UpdatePay(TravelCostDto travelCost)
        {
            var model = mapper.Map<TravelCostEntity>(travelCost);
            db.travelCosts.Update(model);
            db.SaveChanges();
        }
        public void DeletePay(int id)
        {
            var model = db.travelCosts.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                db.travelCosts.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelCostDto GetByIdPay(int id)
        {
            return mapper.Map<TravelCostDto>(db.travelCosts.FirstOrDefault(x => x.id == id));
        }



    }
}
