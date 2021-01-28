using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MacroTrackApi.Models;
using MacroTrackApi.Models.DTOs;
using MacroTrackApi.Models.Entities;
using MacroTrackApi.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace MacroTrackApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository foodRepository;
        private readonly IUserRepository userRepository;
        private readonly IWaterRepository waterRepository;

        public FoodController(IFoodRepository foodRepository, IUserRepository userRepository,
            IWaterRepository waterRepository)
        {
            this.foodRepository = foodRepository;
            this.userRepository = userRepository;
            this.waterRepository = waterRepository;
        }

        [HttpGet("foods/getAllFoods")]
        public async Task<ActionResult<IEnumerable<FoodEntity>>> GetAllFoods()
        {
            var allFoods = await foodRepository.GetAllFoods();

            return new ActionResult<IEnumerable<FoodEntity>>(allFoods);
        }

        [HttpPost("foods/getUserFoodScore")]
        public async Task<ActionResult<UserFoodScoreDTO>> GetUserFoodScore(GetUserFoodsDTO getUserFoods)
        {
            var userFoods = await foodRepository.GetUserFoodsByDate(getUserFoods.UserId, getUserFoods.Date);

            var userFoodScore = new UserFoodScoreDTO
            {
                Calories = 0,
                Protein = 0,
                Fat = 0,
                Carbs = 0,
                Alcohol = 0,
                Fiber = 0,
            };

            foreach (var userFood in userFoods)
            {
                userFoodScore.Calories += userFood.Food.Calories * userFood.Servings;
                userFoodScore.Protein += userFood.Food.Protein * userFood.Servings;
                userFoodScore.Fat += userFood.Food.Fat * userFood.Servings;
                userFoodScore.Carbs += userFood.Food.Carbs * userFood.Servings;
                userFoodScore.Alcohol += userFood.Food.Alcohol * userFood.Servings;
                userFoodScore.Fiber += userFood.Food.Fiber * userFood.Servings;
            }

            return new ActionResult<UserFoodScoreDTO>(userFoodScore);
        }

        [HttpPost("foods/getUserFoods")]
        public async Task<ActionResult<IEnumerable<FoodDTO>>> GetUserFoods(GetUserFoodsDTO getUserFoods)
        {
            var userFoods = await foodRepository.GetUserFoodsByDate(getUserFoods.UserId, getUserFoods.Date);

            return new ActionResult<IEnumerable<FoodDTO>>(userFoods.Select(Mapper.ToFoodDTO));
        }

        [HttpPost("foods/addUserFood")]
        public async Task<ActionResult> AddUserFood(AddUserFoodDTO addUserFoodDTO)
        {
            if (addUserFoodDTO.UserFoods.Length < 1)
            {
                return new AcceptedResult();
            }

            var userFoods = new List<UserFoodEntity>();
            foreach (var userFood in addUserFoodDTO.UserFoods)
            {
                if (userFood.Servings > 0)
                {
                    userFoods.Add(new UserFoodEntity()
                    {
                        UserId = addUserFoodDTO.UserId,
                        FoodId = userFood.FoodId,
                        Servings = userFood.Servings,
                        Created = addUserFoodDTO.Created
                    });
                }
            }

            await foodRepository.UpsertUserFoods(addUserFoodDTO.UserId, userFoods);
            return new AcceptedResult();
        }
        
        
        [HttpPost("foods/updateUserFood")]
        public async Task<ActionResult> UpdateUserFood(UpdateUserFoodDTO updateUserFoodDTO)
        {
            var userFood = 
                new UserFoodEntity()
                {
                    UserId = updateUserFoodDTO.UserId,
                    FoodId = updateUserFoodDTO.UserFood.FoodId,
                    Servings = updateUserFoodDTO.UserFood.Servings,
                    Created = updateUserFoodDTO.Updated
                };

            if (updateUserFoodDTO.UserFood.Servings == 0)
            {
                await foodRepository.RemoveUserFoods(updateUserFoodDTO.UserId, userFood);
                return new AcceptedResult();
            }

            await foodRepository.UpdateUserFood(updateUserFoodDTO.UserId, userFood);
            return new AcceptedResult();
        }

        [HttpPost("foods/addFood")]
        public async Task<ActionResult> AddFood(AddFoodDTO addFoodDTO)
        {
            try
            {
                var food = new FoodEntity()
                {
                    Name = addFoodDTO.Name,
                    Brand = addFoodDTO.Brand,
                    ServingSize = addFoodDTO.ServingSize,
                    Calories = addFoodDTO.Calories,
                    Protein = addFoodDTO.Protein,
                    Fat = addFoodDTO.Fat,
                    Carbs = addFoodDTO.Carbs,
                    Alcohol = addFoodDTO.Alcohol,
                    Fiber = addFoodDTO.Fiber
                };

                await foodRepository.AddFood(food);
                return new AcceptedResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new ConflictResult();
            }
        }
    }
}