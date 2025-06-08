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
            context.usersEntities.Add(usersEntity);
            context.SaveChanges();
        }
        public List<UsersEntity> ShowUsers()
        {
            return context.usersEntities.ToList();
        }
        public void UpdateUser(UsersEntity usersEntity)
        {
            context.usersEntities.Update(usersEntity);
            context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var model = context.usersEntities.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                context.usersEntities.Remove(model);
                context.SaveChanges();
            }
        }
        public UsersEntity GetById(int id)
        {
            return context.usersEntities.FirstOrDefault(x => x.Id == id);
        }
    }
}
