using System;

namespace MacroTrackApi.Models.DTOs
{
    public class LoginDTO
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
    }
}