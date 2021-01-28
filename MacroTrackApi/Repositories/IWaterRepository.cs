using System;
using System.Threading.Tasks;
using MacroTrackApi.Models.Entities;
using System.Collections.Generic;

namespace MacroTrackApi.Repositories
{
    public interface IWaterRepository
    {
        Task<UserWaterEntity> GetUserWater(Guid userId, DateTime date);
        Task UpsertUserWater(UserWaterEntity entity, DateTime date);
        Task<IEnumerable<DrinkEntity>> GetDrinks();
        Task<IEnumerable<UserDrinkEntity>> GetUserDrinks(Guid userId, DateTime date);
        Task UpsertUserDrink(UserDrinkEntity entity, DateTime date);
    }
}