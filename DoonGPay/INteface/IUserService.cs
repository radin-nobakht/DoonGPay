using DoonGPay.Dto;
using DoonGPay.Entity;

namespace DoonGPay.INteface
{
    public interface IUserService
    {
        void AddUser(UserDto user);
        void DeleteUser(int id);
        List<UserDto> Users();
        void UpdateUser(UserDto usersEntity);
        UserDto GetById(int id);
        void Save(UserDto user);
    }
}