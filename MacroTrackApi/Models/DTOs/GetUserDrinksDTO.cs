using System;

namespace MacroTrackApi.Models.DTOs
{
    public class GetUserDrinksDTO
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public DateTime Date { get; set; }
    }
}