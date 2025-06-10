using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Entity;
using DoonGPay.INteface;
using System.Security.Principal;
namespace DoonGPay.Service
{
    public class UserService(MyContext db,IMapper mapper) : IUserService
    {
        public void AddUser(UserDto user)
        {
            var model = mapper.Map<UsersEntity>(user);

            db.usersEntities.Add(model);
            db.SaveChanges();
        }
        public List<UserDto> ShowUsers()
        {
            List<string> ids = db.usersEntities.ToList().Select(x => x.Name + " " + x.LName).ToList();

            var data = db.usersEntities.ToList();

            return mapper.Map<List<UserDto>>(data);


            return data.Select(x => new UserDto
            {
                Id = x.Id,
                LName = x.LName,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber
            }).ToList();
        }
        public void UpdateUser(UserDto user)
        {
            var model = mapper.Map<UsersEntity>(user);
            db.usersEntities.Update(model);
            db.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var model = db.usersEntities.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.usersEntities.Remove(model);
                db.SaveChanges();
            }
        }
        public UserDto GetById(int id)
        {
            return mapper.Map<UserDto>(db.usersEntities.FirstOrDefault(x => x.Id == id));
        }

        public void Save(UserDto user)
        {
            var model = mapper.Map<UsersEntity>(user);
            if (model.Id > 0)
                db.Add(model);
            else db.Update(model);
            db.SaveChanges();
        }
    }
}
