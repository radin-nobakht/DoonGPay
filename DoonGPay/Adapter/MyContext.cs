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
        public DbSet<UsersEntity> usersEntities { get; set; }
        public DbSet<PayEntity> payEntities { get; set; }
        public DbSet<FriendEntity> friendsEntities { get; set; }
        public DbSet<MainTableEntity> mainTablesEntities { get; set; }
    }
}
