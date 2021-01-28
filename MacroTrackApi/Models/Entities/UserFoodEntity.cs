using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MacroTrackApi.Models.Entities
{
    [Table("userFoods")]
    public class UserFoodEntity
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public UserEntity User { get; set; }
        public long FoodId { get; set; }
        public FoodEntity Food { get; set; }
        public int Servings { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}