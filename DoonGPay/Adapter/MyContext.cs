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
        public DbSet<TravelPayEntity> travelPayEntity { get; set; }
        public DbSet<TravelFellowTravelerEntity> travelFellowTravelerEntity { get; set; }
        public DbSet<TravelEntity> travelEntity { get; set; }
    }
}
