using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MacroTrackApi.Models;
using MacroTrackApi.Models.Entities;

namespace MacroTrackApi.Repositories
{
    public interface IFoodRepository
    {
        Task AddFood(FoodEntity entity);
        Task<IEnumerable<FoodEntity>> GetAllFoods();
        Task<IEnumerable<UserFoodEntity>> GetUserFoods(Guid userId);
        Task<IEnumerable<UserFoodEntity>> GetUserFoodsByDate(Guid userId, DateTime date);
        Task UpsertUserFoods(Guid userId, IEnumerable<UserFoodEntity> entities);
        Task UpdateUserFood(Guid userId, UserFoodEntity entity);
        Task RemoveUserFoods(Guid userId, UserFoodEntity entity);
    }
}