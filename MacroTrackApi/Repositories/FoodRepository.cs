using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MacroTrackApi.Models;
using MacroTrackApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MacroTrackApi.Repositories
{
    public class FoodRepository : IFoodRepository
    {
        public FoodContext foodContext;

        public FoodRepository(FoodContext foodContext)
        {
            this.foodContext = foodContext;
        }

        public async Task AddFood(FoodEntity entity)
        {
            entity.Created = DateTime.UtcNow;
            foodContext.Food.Add(entity);
            await foodContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FoodEntity>> GetAllFoods()
        {
            return await foodContext.Food.ToListAsync();
        }

        public async Task<IEnumerable<UserFoodEntity>> GetUserFoods(Guid userId)
        {
            return await foodContext.UserFood
                .Include(e => e.Food)
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserFoodEntity>> GetUserFoodsByDate(Guid userId, DateTime date)
        {
            return await foodContext.UserFood
                .Include(e => e.Food)
                .Where(e => e.UserId == userId && e.Created.Date == date.Date)
                .ToListAsync();
        }

        public async Task UpsertUserFoods(Guid userId, IEnumerable<UserFoodEntity> entities)
        {
            var userFoods = await foodContext.UserFood
                .Include(e => e.Food)
                .Where(e => e.UserId == userId && e.Created.Date == entities.First().Created.Date)
                .ToDictionaryAsync(e => e.FoodId, e => e);

            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    continue;
                }
                
                if (userFoods.ContainsKey(entity.FoodId))
                {
                    var food = userFoods[entity.FoodId];
                    food.Servings += entity.Servings;
                    foodContext.UserFood.Update(food);
                }
                else
                {
                    foodContext.UserFood.Add(entity);
                }
            }
            await foodContext.SaveChangesAsync();
        }
        
        public async Task UpdateUserFood(Guid userId, UserFoodEntity entity)
        {
            var userFood = await foodContext.UserFood
                .Include(e => e.Food)
                .Where(e => e.UserId == userId && e.Created.Date == entity.Created.Date && e.FoodId == entity.FoodId).SingleOrDefaultAsync();

            userFood.Servings = entity.Servings;
            
            foodContext.UserFood.Update(userFood);
            await foodContext.SaveChangesAsync();
        }
        public async Task RemoveUserFoods(Guid userId, UserFoodEntity entity)
        {
            var userFood = foodContext.UserFood
                .Include(e => e.Food)
                .Where(e => e.UserId == userId && e.Created.Date == entity.Created.Date && e.FoodId == entity.FoodId);
            
            foodContext.UserFood.RemoveRange(userFood);
            await foodContext.SaveChangesAsync();
        }
    }
}