using System.Threading.Tasks;
using MacroTrackApi.Models.DTOs;
using MacroTrackApi.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MacroTrackApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class WaterController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IWaterRepository waterRepository;

        public WaterController(IUserRepository userRepository, IWaterRepository waterRepository)
        {
            this.userRepository = userRepository;
            this.waterRepository = waterRepository;
        }

        // Depreciated 
        [HttpPost("water/getUserWater")]
        public async Task<ActionResult<UserWaterDTO>> GetUserWater(GetUserWaterDTO getUserWaterDTO)
        {
            var userWater = await waterRepository.GetUserWater(getUserWaterDTO.UserId, getUserWaterDTO.Date);

            return userWater == null ? new ActionResult<UserWaterDTO>(new EmptyResult()) : new ActionResult<UserWaterDTO>(Mapper.ToUserWaterDTO(userWater));
        }

        // Depreciated 
        [HttpPost("water/upsertUserWater")]
        public async Task<ActionResult> UpsertUserWater(UpdateUserWaterDTO updateUserWaterDTO)
        {
            await waterRepository.UpsertUserWater(Mapper.ToUserWaterEntity(updateUserWaterDTO), updateUserWaterDTO.Date);

            return new AcceptedResult();
        }

        // Depreciated 
        [HttpPost("water/addOneWater")]
        public async Task<ActionResult> AddOneWater(UpdateUserWaterDTO updateUserWaterDTO)
        {
            var userWater = await waterRepository.GetUserWater(updateUserWaterDTO.UserId, updateUserWaterDTO.Date);

            if (userWater != null)
            {
                updateUserWaterDTO.Amount = userWater.Amount + 240;
            }
            else
            {
                updateUserWaterDTO.Amount = 240;
            }

            var drink = new UpdateUserDrinkDTO()
            {
                UserId = updateUserWaterDTO.UserId,
                DrinkId = 1,
                Amount = updateUserWaterDTO.Amount
            };

            await waterRepository.UpsertUserDrink(Mapper.ToUserDrinkEntity(drink), updateUserWaterDTO.Date);

            return new AcceptedResult();
        }

        [HttpPost("water/getDrinks")]
        public async Task<ActionResult<IEnumerable<DrinkDTO>>> getUserDrinks(DefaultPostDTO defaultPostDTO)
        {
            if (!await Helpers.CheckAccessToken(userRepository, defaultPostDTO.UserId, defaultPostDTO.Code))
            {
                return new UnauthorizedResult();
            }

            var drinks = await waterRepository.GetDrinks();

            return new ActionResult<IEnumerable<DrinkDTO>>(drinks.Select(Mapper.ToDrinkDTO));
        }

        [HttpPost("water/getUserDrinks")]
        public async Task<ActionResult> getUserDrinks(GetUserDrinksDTO getUserDrinksDTO)
        {
            if (!await Helpers.CheckAccessToken(userRepository, getUserDrinksDTO.UserId, getUserDrinksDTO.Code))
            {
                return new UnauthorizedResult();
            }

            await waterRepository.GetUserDrinks(getUserDrinksDTO.UserId, getUserDrinksDTO.Date);

            return new AcceptedResult();
        }

        [HttpPost("water/upsertUserDrink")]
        public async Task<ActionResult> UpsertUserDrink(UpdateUserDrinkDTO updateUserDrinkDTO)
        {
            if (!await Helpers.CheckAccessToken(userRepository, updateUserDrinkDTO.UserId, updateUserDrinkDTO.Code))
            {
                return new UnauthorizedResult();
            }

            await waterRepository.UpsertUserDrink(Mapper.ToUserDrinkEntity(updateUserDrinkDTO), updateUserDrinkDTO.Date);

            return new AcceptedResult();
        }
    }
}