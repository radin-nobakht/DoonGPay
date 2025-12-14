using AutoMapper;
using DoonGPay.Adapter;
using DoonGPay.Dto;
using DoonGPay.Dto.Travel;
using DoonGPay.Entity;
using DoonGPay.Helpers;
using DoonGPay.INteface;
using DoonGPay.ViewModel;
using System.Reflection.Metadata;
using System.Security.Principal;
namespace DoonGPay.Service
{
    public class UserService(MyContext db,IMapper mapper) : IUserService
    {
        public void AddUser(UserDto user)
        {
            // مرحله ۱: تبدیل Dto به Entity (به جز تصویر)
            var model = mapper.Map<UserEntity>(user);

            // مرحله ۲: هش کردن پسورد
            model.Password = PasswordHelper.HashPassword(model.Password);

            // مرحله ۳: بررسی و تبدیل عکس در صورت وجود
            if (user.TravelImage != null && user.TravelImage.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    user.TravelImage.CopyTo(ms);
                    model.TravelImage = ms.ToArray(); // تبدیل به byte[]
                }
            }

            // مرحله ۴: ذخیره در دیتابیس
            db.Users.Add(model);
            db.SaveChanges();
        }
        public List<UserDto> Users() => mapper.Map<List<UserDto>>(db.Users.ToList());
        
        public PagationViewModel<UserDto> UserList(int pageNum, string url)
        {

            pageNum = pageNum == 0 ? 1 : pageNum;
            var count = db.Users.Count();

            int skipNum = pageNum - 1;
            skipNum = skipNum * 5;
            var list = db.Users.Skip(skipNum).Take(5).ToList();
            return new PagationViewModel<UserDto>
            {
                List = mapper.Map<List<UserDto>>(list),
                PagationModel = new PagationModel
                {
                    TotalItem = count,
                    CurrentPage = pageNum,
                    Url = url
                }

            };

        }
        public List<UserDto> User()
        {
            //List<string> ids = db.Users.ToList().Select(x => x.Name + " " + x.LName).ToList();

            var data = db.Users.ToList();

            return mapper.Map<List<UserDto>>(data); ;


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
