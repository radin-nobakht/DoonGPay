using DoonGPay.Adapter;
using DoonGPay.Entity;
using DoonGPay.INteface;

namespace DoonGPay.Service
{
    public class TravelService(MyContext db) : ITravelService
    {
        public void AddTravel(TravelEntity travelEntity)
        {
            db.travelEntity.Add(travelEntity);
            db.SaveChanges();
        }
        public List<TravelEntity> ShowTravel()
        {
            return db.travelEntity.ToList();
        }
        public void UpdateTravel(TravelEntity travelEntity)
        {
            db.travelEntity.Update(travelEntity);
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
        public TravelEntity GetById(int id)
        {
            return db.travelEntity.FirstOrDefault(x => x.Id == id);
        }
        public void AddFellowTraveler(TravelFellowTravelerEntity TravelFellowTravelerEntity)

        {
            db.travelFellowTravelerEntity.Add(TravelFellowTravelerEntity);
            db.SaveChanges();
        }
        public List<TravelFellowTravelerEntity> ShowFellowTraveler()
        {
            return db.travelFellowTravelerEntity.ToList();
        }
        public void UpdateFellowTraveler(TravelFellowTravelerEntity TravelFellowTravelerEntity)
        {
            db.travelFellowTravelerEntity.Update(TravelFellowTravelerEntity);
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
        public TravelFellowTravelerEntity GetByIdFellowTraveler(int id)
        {
            return db.travelFellowTravelerEntity.FirstOrDefault(x => x.Id == id);
        }
        public void AddPay(TravelPayEntity travelPayEntity)
        {
            db.travelPayEntity.Add(travelPayEntity);
            db.SaveChanges();
        }
        public List<TravelPayEntity> ShowPay()
        {
            return db.travelPayEntity.ToList();
        }
        public void UpdatePay(TravelPayEntity travelPayEntity)
        {
            db.travelPayEntity.Update(travelPayEntity);
            db.SaveChanges();
        }
        public void DeletePay(int id)
        {
            var model = db.travelPayEntity.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                db.travelPayEntity.Remove(model);
                db.SaveChanges();
            }
        }
        public TravelPayEntity GetByIdPay(int id)
        {
            return db.travelPayEntity.FirstOrDefault(x => x.id == id);
        }



    }
}
