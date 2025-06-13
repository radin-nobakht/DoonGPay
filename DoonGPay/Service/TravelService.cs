using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.INteface;


namespace DoonGPay.Service
{
    public class TravelService(MyContext db,IMapper mapper) : ITravelService
    {
        public void AddTravel(TravelDto travel)
        {
            var model = mapper.Map<TravelEntity>(travel);

            db.travelEntity.Add(model);
            db.SaveChanges();
        }
        public List<TravelDto> ShowTravel()
        {

            var data = db.travelEntity.ToList();

            return mapper.Map<List<TravelDto>>(data);
        }
        public void UpdateTravel(TravelDto travel)
        {
            var model = mapper.Map<TravelEntity>(travel);
            db.travelEntity.Update(model);
            db.SaveChanges();
        }
        public void DeleteTravel(int id)
        {
            var model = db.travelEntity.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.travelEntity.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelDto GetById(int id)
        {
            return mapper.Map<TravelDto>(db.travelEntity.FirstOrDefault(x => x.Id == id));
        }
        public void AddFellowTraveler(TravelFellowTravelerDto TravelFellowTraveler)
        {
            var model = mapper.Map<TravelFellowTravelerEntity>(TravelFellowTraveler);

            db.travelFellowTravelerEntity.Add(model);
            db.SaveChanges();
        }
        public List<TravelFellowTravelerDto> ShowFellowTraveler()
        {

            var data = db.travelFellowTravelerEntity.ToList();

            return mapper.Map<List<TravelFellowTravelerDto>>(data);
        }
        public void UpdateFellowTraveler(TravelFellowTravelerDto TravelFellowTraveler)
        {
            var model = mapper.Map<TravelFellowTravelerEntity>(TravelFellowTraveler);
            db.travelFellowTravelerEntity.Update(model);
            db.SaveChanges();
        }
        public void DeleteFellowTraveler(int id)
        {
            var model = db.travelFellowTravelerEntity.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.travelFellowTravelerEntity.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelFellowTravelerDto GetByIdFellowTraveler(int id)
        {
            return mapper.Map<TravelFellowTravelerDto>(db.travelFellowTravelerEntity.FirstOrDefault(x => x.Id == id));
        }
        public void AddPay(TravelCostDto travelPay)
        {
            var model = mapper.Map<TravelCostEntity>(travelPay);

            db.TravelCostEntity.Add(model);
            db.SaveChanges();
        }
        public List<TravelCostDto> ShowPay()
        {

            var data = db.TravelCostEntity.ToList();

            return mapper.Map<List<TravelCostDto>>(data);
        }
        public void UpdatePay(TravelCostDto travelPay)
        {
            var model = mapper.Map<TravelCostEntity>(travelPay);
            db.TravelCostEntity.Update(model);
            db.SaveChanges();
        }
        public void DeletePay(int id)
        {
            var model = db.TravelCostEntity.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                db.TravelCostEntity.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelCostDto GetByIdPay(int id)
        {
            return mapper.Map<TravelCostDto>(db.TravelCostEntity.FirstOrDefault(x => x.id == id));
        }



    }
}
