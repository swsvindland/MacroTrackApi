using System;

namespace MacroTrackApi.Models.DTOs
{
    public class GetUserWaterDTO
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}