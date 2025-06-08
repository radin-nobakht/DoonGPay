using DoonGPay.Adapter;
using DoonGPay.Entity;
using DoonGPay.INteface;

namespace DoonGPay.Service
{
    public class PayService(MyContext context) : IPayService
    {
        public void Add(PayEntity payEntity)
        {
            context.payEntities.Add(payEntity);
            context.SaveChanges();
        }
        public List<PayEntity> Shows()
        {
            return context.payEntities.ToList();
        }
        public void Update(PayEntity payEntity)
        {
            context.payEntities.Update(payEntity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.payEntities.FirstOrDefault(x => x.id == id);
            if (model != null)
            {
                context.payEntities.Remove(model);
                context.SaveChanges();
            }
        }
        public PayEntity GetById(int id)
        {
            return context.payEntities.FirstOrDefault(x => x.id == id);
        }
    }
}
