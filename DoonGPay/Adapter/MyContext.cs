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
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TravelCostEntity> TravelCosts { get; set; }
        public DbSet<TravelFellowtravelerEntity> TravelFellowtravelers { get; set; }
        public DbSet<TravelEntity> Travels { get; set; }
    }
}
