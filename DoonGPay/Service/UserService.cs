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
            var model = mapper.Map<UserEntity>(user);

            db.Users.Add(model);
            db.SaveChanges();
        }
        public List<UserDto> Users()
        {
            //List<string> ids = db.Users.ToList().Select(x => x.Name + " " + x.LName).ToList();

            var data = db.Users.ToList();

            return mapper.Map<List<UserDto>>(data);


            //return data.Select(x => new UserDto
            //{
            //    Id = x.Id,
            //    LastName = x.LastName,
            //    FristName = x.FristName,
            //    PhoneNumber = x.PhoneNumber
            //}).ToList();
        }
        public void UpdateUser(UserDto user)
        {
            var model = mapper.Map<UserEntity>(user);
            db.Users.Update(model);
            db.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            var model = db.Users.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                db.Users.Remove(model);
                db.SaveChanges();
            }
        }
        public UserDto GetById(int id)
        {
            return mapper.Map<UserDto>(db.Users.FirstOrDefault(x => x.Id == id));
        }

        public void Save(UserDto user)
        {
            var model = mapper.Map<UserEntity>(user);
            if (model.Id > 0)
                db.Add(model);
            else db.Update(model);
            db.SaveChanges();
        }
    }
}
