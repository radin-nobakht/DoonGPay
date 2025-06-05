using DoonGPay.Adapter;
using DoonGPay.Entity;
using DoonGPay.INteface;
using System.Security.Principal;
namespace DoonGPay.Service
{
    public class UserService(MyContext context) : IUserService
    {
        public void AddUser(UsersEntity usersEntity)
        {
            context.usrsEntities.Add(usersEntity);
            context.SaveChanges();
        }
        public List<UsersEntity> ShowUsers()
        {
            return context.usrsEntities.ToList();
        }
        public void UpdateUser(UsersEntity usersEntity)
        {
            context.usrsEntities.Update(usersEntity);
            context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var model = context.usrsEntities.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                context.usrsEntities.Remove(model);
                context.SaveChanges();
            }
        }
        public UsersEntity GetById(int id)
        {
            return context.usrsEntities.FirstOrDefault(x => x.Id == id);
        }
    }
}
