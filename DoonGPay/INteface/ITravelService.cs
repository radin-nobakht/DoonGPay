using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface ITravelService
    {
        void AddFellowTraveler(TravelFellowTravelerEntity travelFellowTravelerEntity);
        void AddPay(TravelPayEntity travelPayEntity);
        void AddTravel(TravelEntity travelEntity);
        void DeleteFellowTraveler(int id);
        void DeletePay(int id);
        void DeleteTravel(int id);
        TravelEntity GetById(int id);
        TravelFellowTravelerEntity GetByIdFellowTraveler(int id);
        TravelPayEntity GetByIdPay(int id);
        List<TravelFellowTravelerEntity> ShowFellowTraveler();
        List<TravelPayEntity> ShowPay();
        List<TravelEntity> ShowTravel();
        void UpdateTravel(TravelEntity travelEntity);
        void UpdateFellowTraveler(TravelFellowTravelerEntity TravelFellowTravelerEntity);
        void UpdatePay(TravelPayEntity travelPayEntity);
    }
}