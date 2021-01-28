using Microsoft.EntityFrameworkCore;
 using MacroTrackApi.Models.Entities;
 
 namespace MacroTrackApi.Models
 {
     public class FoodContext : DbContext
     {
         public FoodContext(DbContextOptions<FoodContext> options): base(options)
         {
         }
 
         public DbSet<FoodEntity> Food { get; set; }
         public DbSet<UserFoodEntity> UserFood { get; set; }
     }
 }