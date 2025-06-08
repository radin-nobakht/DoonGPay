using DoonGPay.Adapter;
using DoonGPay.Entity;

namespace DoonGPay.Service
{
    public class friendService(MyContext context)
    {
        public void Add(FriendEntity friendEntity)
        {
            context.friendsEntities.Add(friendEntity);
            context.SaveChanges();
        }
        public List<FriendEntity> Shows()
        {
            return context.friendsEntities.ToList();
        }
        public void Update(FriendEntity friendEntity)
        {
            context.friendsEntities.Update(friendEntity);
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = context.friendsEntities.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                context.friendsEntities.Remove(model);
                context.SaveChanges();
            }
        }
        public FriendEntity GetById(int id)
        {
            return context.friendsEntities.FirstOrDefault(x => x.Id == id);
        }
    }
}
