using System;
using System.Threading.Tasks;
using MacroTrackApi.Models;
using MacroTrackApi.Models.DTOs;
using MacroTrackApi.Models.Entities;
using MacroTrackApi.Repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using MacroTrackApi.Utils;


namespace MacroTrackApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("users/sendLoginCode")]
        public async Task<ActionResult> SendLoginCode(SendLoginCodeDTO sendLoginCodeDTO)
        {
            var user = await userRepository.GetUserByEmail(sendLoginCodeDTO.Email);

            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var rand = new Random();
            var code = rand.Next(999999);
            await userRepository.AddUserAuthCode(user.Id, code);

            TwilioClient.Init(JsonUtils.LoadSecrets().TwilioAccountSid, JsonUtils.LoadSecrets().TwilioAuthToken);

            var message = await MessageResource.CreateAsync(
                body: $"Your Login Code for <>track is {code} - Code will expire in 5 min",
                from: new Twilio.Types.PhoneNumber("+12058803770"),
                to: new Twilio.Types.PhoneNumber($"+{user.PhoneNumber}")
            );

            return new AcceptedResult();
        }

        [HttpPost("users/getUserLogin")]
        public async Task<ActionResult<LoginDTO>> GetUserLogin(GetUserLoginDTO getUserLoginDTO)
        {
            var user = await userRepository.GetUserByEmail(getUserLoginDTO.Email);

            if (user == null)
            {
                return new UnauthorizedResult();
            }

            var (valid, reason) = await userRepository.VerifyUserAuthCode(user.Id, int.Parse(getUserLoginDTO.Code));

            if (!valid) return new UnauthorizedResult();
            var accessToken = Helpers.CreateAccessToken(user.Id);

            await userRepository.AddAccessToken(user.Id, accessToken);

            return new LoginDTO()
            {
                Id = user.Id,
                Token = accessToken
            };
        }

        [HttpPost("users/getUser")]
        public async Task<ActionResult<UserDTO>> GetUser(GetUserDTO getUserDTO)
        {
            if (!await Helpers.CheckAccessToken(userRepository, getUserDTO.UserId, getUserDTO.Token))
            {
                return new UnauthorizedResult();
            }

            var user = await userRepository.GetUser(getUserDTO.UserId);

            return new ActionResult<UserDTO>(Mapper.ToUserDTO(user));
        }

        [HttpPost("users/addUser")]
        public async Task<ActionResult<UserDTO>> AddUser(AddUserDTO addUserDTO)
        {
            var user = new UserEntity()
            {
                Id = Guid.NewGuid(),
                Name = addUserDTO.Name,
                Email = addUserDTO.Email,
                PhoneNumber = addUserDTO.PhoneNumber,
                PasswordHashed = ""
            };

            await userRepository.AddUser(user);

            var fullUser = await userRepository.GetUserByEmail(user.Email);

            return new ActionResult<UserDTO>(Mapper.ToUserDTO(fullUser));
        }
    }
}