using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MacroTrackApi.Models.Entities;

namespace MacroTrackApi.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetUser(Guid userId);
        Task<UserEntity> GetUserByEmail(string email);
        Task AddUserAuthCode(Guid userId, int code);
        Task<(Boolean, string)> VerifyUserAuthCode(Guid userId, int code);
        Task AddAccessToken(Guid userId, string code);
        Task<bool> VerifyAccessToken(Guid userId, string code);
        Task AddUser(UserEntity user);
        Task<List<UserEntity>> GetAllUsers();
        Task<UserEntity> GetUserByEmailPassword(string email, string password);
    }
}
