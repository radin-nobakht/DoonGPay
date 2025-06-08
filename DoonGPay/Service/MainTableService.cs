using DoonGPay.Adapter;
using DoonGPay.Entity;
using DoonGPay.INteface;

namespace DoonGPay.Service
{
    public class MainTableService(MyContext context) : IMainTableService
    {
        public void Add(MainTableEntity mainTableEntity)
        {
            context.mainTablesEntities.Add(mainTableEntity);
            context.SaveChanges();
        }
        public List<MainTableEntity> Shows()
        {
            return context.mainTablesEntities.ToList();
        }
        public void Update(MainTableEntity mainTableEntity)
        {
            context.mainTablesEntities.Update(mainTableEntity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.mainTablesEntities.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                context.mainTablesEntities.Remove(model);
                context.SaveChanges();
            }
        }
        public MainTableEntity GetById(int id)
        {
            return context.mainTablesEntities.FirstOrDefault(x => x.Id == id);
        }
    }
}
