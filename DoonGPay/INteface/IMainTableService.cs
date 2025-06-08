using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface IMainTableService
    {
        void Add(MainTableEntity MainTableEntity);
        void Delete(int id);
        MainTableEntity GetById(int id);
        List<MainTableEntity> Shows();
        void Update(MainTableEntity MainTableEntity);
    }
}