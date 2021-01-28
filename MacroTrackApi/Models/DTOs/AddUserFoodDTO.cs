using System;

namespace MacroTrackApi.Models.DTOs
{
    public class AddUserFoodDTO
    {
        public Guid UserId { get; set; }
        public UserFoodDTO[] UserFoods { get; set; }
        public DateTime Created { get; set; }
    }
}