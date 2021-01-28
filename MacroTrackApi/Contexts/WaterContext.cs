using Microsoft.EntityFrameworkCore;
using MacroTrackApi.Models.Entities;

namespace MacroTrackApi.Models
{
    public class WaterContext : DbContext
    {
        public WaterContext(DbContextOptions<WaterContext> options) : base(options)
        {
        }

        public DbSet<UserWaterEntity> UserWater { get; set; }
        public DbSet<DrinkEntity> Drinks { get; set; }
        public DbSet<UserDrinkEntity> UserDrinks { get; set; }
    }
}