using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface IUserService
    {
        void AddUser(UsersEntity usersEntity);
        void DeleteUser(int id);
        List<UsersEntity> ShowUsers();
        void UpdateUser(UsersEntity usersEntity);
        UsersEntity GetById(int id);
    }
}