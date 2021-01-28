using System;

namespace MacroTrackApi.Models.DTOs
{
    public class UserWaterDTO
    {
        public Guid UserId { get; set; }
        public int Amount { get; set; }
    }
}