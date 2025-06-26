using DoonGPay.Dto.Travel;

namespace DoonGPay.Inteface.Travel
{
    public interface ITravelService
    {
        void Addtravel(TravelDto travel);
        void Deletetravel(int id);
        TravelDto Travel(int id);
        List<TravelDto> Travels();
        void Updatetravel(TravelDto travel);
    }
}