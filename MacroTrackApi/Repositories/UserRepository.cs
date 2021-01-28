using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MacroTrackApi.Models;
using MacroTrackApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MacroTrackApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        public UserContext userContext;

        public UserRepository(UserContext userContext)
        {
            this.userContext = userContext;
        }

        public async Task<UserEntity> GetUser(Guid userId)
        {
            return await userContext.User.SingleOrDefaultAsync(e => e.Id == userId);
        }

        public async Task<UserEntity> GetUserByEmail(string email)
        {
            return await userContext.User.SingleOrDefaultAsync(e => e.Email == email);
        }

        public async Task AddUserAuthCode(Guid userId, int code)
        {
            var entity = new AuthCodeEntity()
            {
                UserId = userId,
                Code = code,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

            userContext.AuthCode.Add(entity);

            await userContext.SaveChangesAsync();
        }

        public async Task<(Boolean, string)> VerifyUserAuthCode(Guid userId, int code)
        {
            var entities = await userContext.AuthCode.Where(e => e.UserId == userId && e.Created.Date == DateTime.UtcNow.Date).ToListAsync();

            foreach (var entity in entities)
            {
                if (entity.Code == code)
                {
                    if (entity.Expires > DateTime.UtcNow)
                    {
                        return (true, null);
                    }
                    else
                    {
                        return (false, "Code is Expired");
                    }
                }
            }

            return (false, "Code not Found");
        }

        public async Task AddAccessToken(Guid userId, string code)
        {
            var entity = new AccessTokenEntity()
            {
                UserId = userId,
                Code = code,
                Created = DateTime.UtcNow
            };

            userContext.AccessToken.Add(entity);

            await userContext.SaveChangesAsync();
        }

        public async Task<bool> VerifyAccessToken(Guid userId, string code)
        {
            var entity = await userContext.AccessToken.FirstOrDefaultAsync(e => e.UserId == userId && e.Code == code);

            if (entity != null)
            {
                return true;
            }

            return false;
        }

        public async Task AddUser(UserEntity user)
        {
            user.Created = DateTime.UtcNow;
            userContext.User.Add(user);
            await userContext.SaveChangesAsync();
        }

        public async Task<List<UserEntity>> GetAllUsers()
        {
            return await userContext.User.ToListAsync();
        }



        public async Task<UserEntity> GetUserByEmailPassword(string email, string password)
        {
            return await userContext.User.FirstOrDefaultAsync(e => e.Email.ToLower() == email.ToLower() && e.PasswordHashed == password);
        }
    }
}
