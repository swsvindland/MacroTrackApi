using System;

namespace MacroTrackApi.Models.Entities
{
    public class UserWaterEntity
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public int Amount { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}