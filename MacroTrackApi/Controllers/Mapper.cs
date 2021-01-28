using MacroTrackApi.Models.DTOs;
using MacroTrackApi.Models.Entities;

namespace MacroTrackApi.Controllers
{
    public class Mapper
    {
        public static UserDTO ToUserDTO(UserEntity userEntity)
        {
            return new UserDTO
            {
                Id = userEntity.Id,
                Name = userEntity.Name,
                Email = userEntity.Email,
                PhoneNumber = userEntity.PhoneNumber
            };
        }

        public static FoodDTO ToFoodDTO(UserFoodEntity userFoodEntity)
        {
            return new FoodDTO
            {
                Id = userFoodEntity.Food.Id,
                Servings = userFoodEntity.Servings,
                Name = userFoodEntity.Food.Name,
                Brand = userFoodEntity.Food.Brand,
                ServingSize = userFoodEntity.Food.ServingSize,
                Calories = userFoodEntity.Food.Calories * userFoodEntity.Servings,
                Protein = userFoodEntity.Food.Protein * userFoodEntity.Servings,
                Fat = userFoodEntity.Food.Fat * userFoodEntity.Servings,
                Carbs = userFoodEntity.Food.Carbs * userFoodEntity.Servings,
                Alcohol = userFoodEntity.Food.Alcohol * userFoodEntity.Servings,
                Fiber = userFoodEntity.Food.Fiber * userFoodEntity.Servings
            };
        }

        public static UserWaterEntity ToUserWaterEntity(UpdateUserWaterDTO userWaterDTO)
        {
            return new UserWaterEntity()
            {
                UserId = userWaterDTO.UserId,
                Amount = userWaterDTO.Amount,
            };
        }

        public static UserWaterDTO ToUserWaterDTO(UserWaterEntity entity)
        {
            return new UserWaterDTO()
            {
                UserId = entity.UserId,
                Amount = entity.Amount,
            };
        }

        public static UserDrinkEntity ToUserDrinkEntity(UpdateUserDrinkDTO updateUserDrinkDTO)
        {
            return new UserDrinkEntity()
            {
                UserId = updateUserDrinkDTO.UserId,
                DrinkId = updateUserDrinkDTO.DrinkId,
                Amount = updateUserDrinkDTO.Amount,
            };
        }

        public static DrinkDTO ToDrinkDTO(DrinkEntity entity)
        {
            return new DrinkDTO()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}