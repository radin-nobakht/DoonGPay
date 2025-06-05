using Microsoft.EntityFrameworkCore;
using DoonGPay.Entity;

namespace DoonGPay.Adapter
{
    public class MyContext:DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
      : base(options)
        {
            Database.SetCommandTimeout(30);
        }
        public DbSet<UsersEntity> usrsEntities { get; set; }
    }
}
