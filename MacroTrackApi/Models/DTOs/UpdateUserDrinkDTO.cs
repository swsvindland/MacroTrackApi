using System;

namespace MacroTrackApi.Models.DTOs
{
    public class UpdateUserDrinkDTO
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
        public int DrinkId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
    }
}