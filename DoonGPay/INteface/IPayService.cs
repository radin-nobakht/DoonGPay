using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface IPayService
    {
        void Add(PayEntity PayEntity);
        void Delete(int id);
        PayEntity GetById(int id);
        List<PayEntity> Shows();
        void Update(PayEntity PayEntity);
    }
}