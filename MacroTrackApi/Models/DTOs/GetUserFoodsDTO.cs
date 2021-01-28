using System;

namespace MacroTrackApi.Models
{
    public class GetUserFoodsDTO
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
    }
}