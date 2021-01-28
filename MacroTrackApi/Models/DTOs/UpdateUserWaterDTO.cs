using System;

namespace MacroTrackApi.Models.DTOs
{
    public class UpdateUserWaterDTO
    {
        public Guid UserId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}