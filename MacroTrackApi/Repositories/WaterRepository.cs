using System;
using System.Threading.Tasks;
using MacroTrackApi.Models;
using MacroTrackApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace MacroTrackApi.Repositories
{
    public class WaterRepository : IWaterRepository
    {
        private WaterContext waterContext;

        public WaterRepository(WaterContext waterContext)
        {
            this.waterContext = waterContext;
        }

        public async Task<UserWaterEntity> GetUserWater(Guid userId, DateTime date)
        {
            var userWater =
                await waterContext.UserWater.FirstOrDefaultAsync(e =>
                    e.UserId == userId && e.Created.Date == date.Date);

            return userWater;
        }

        public async Task UpsertUserWater(UserWaterEntity entity, DateTime date)
        {
            var userWater =
                await waterContext.UserWater.FirstOrDefaultAsync(e =>
                    e.UserId == entity.UserId && e.Created.Date == date.Date);

            if (userWater == null)
            {
                entity.Created = date;
                entity.Updated = null;
                waterContext.UserWater.Add(entity);
            }
            else
            {
                userWater.Updated = date;
                userWater.Amount = entity.Amount;
            }

            await waterContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DrinkEntity>> GetDrinks()
        {
            return await waterContext.Drinks.ToListAsync();
        }

        public async Task<IEnumerable<UserDrinkEntity>> GetUserDrinks(Guid userId, DateTime date)
        {
            return await waterContext.UserDrinks.Where(e => e.UserId == userId && date.Date == date.Date).ToListAsync();
        }

        public async Task UpsertUserDrink(UserDrinkEntity entity, DateTime date)
        {
            var userDrink =
                await waterContext.UserDrinks.FirstOrDefaultAsync(e =>
                    e.UserId == entity.UserId && e.DrinkId == entity.DrinkId && e.Created.Date == date.Date);

            if (userDrink == null)
            {
                entity.Created = date;
                entity.Updated = null;
                waterContext.UserDrinks.Add(entity);
            }
            else
            {
                userDrink.Updated = date;
                userDrink.Amount = entity.Amount;
            }

            await waterContext.SaveChangesAsync();
        }

    }
}