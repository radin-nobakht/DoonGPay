using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.ViewModel;

namespace DoonGPay.INteface
{
    public interface IUserService
    {
        void AddUser(UserDto user);
        void DeleteUser(int id);
        PagationViewModel<UserDto> UserList(int pageNum, string url);
        void UpdateUser(UserDto usersEntity);
        UserDto GetById(int id);
        void Save(UserDto user);
        List<UserDto> User();
    }
}