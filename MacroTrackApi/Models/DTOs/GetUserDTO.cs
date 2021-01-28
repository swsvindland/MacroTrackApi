using System;

namespace MacroTrackApi.Models
{
    public class GetUserDTO
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}