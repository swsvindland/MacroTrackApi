using Microsoft.EntityFrameworkCore;
using MacroTrackApi.Models.Entities;

namespace MacroTrackApi.Models
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<AuthCodeEntity> AuthCode { get; set; }
        public DbSet<AccessTokenEntity> AccessToken { get; set; }
    }
}
