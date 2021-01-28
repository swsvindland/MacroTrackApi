using System;
using System.Text;
using System.Security.Cryptography;
using MacroTrackApi.Repositories;
using System.Threading.Tasks;

namespace MacroTrackApi.Controllers
{
    public static class Helpers
    {
        public static string CreateAccessToken(Guid userId)
        {
            var token = userId.ToString() + Guid.NewGuid().ToString() + Guid.NewGuid().ToString();

            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(token));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }


        public static async Task<bool> CheckAccessToken(IUserRepository userRepository, Guid userId, string token)
        {
            return await userRepository.VerifyAccessToken(userId, token);
        }
    }
}