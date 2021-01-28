using System;

namespace MacroTrackApi.Models.DTOs
{
    public class UpdateUserFoodDTO
    {
        public Guid UserId { get; set; }
        public UserFoodDTO UserFood { get; set; }
        public DateTime Updated { get; set; }
    }
}