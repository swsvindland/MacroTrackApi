using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MacroTrackApi.Models.Entities
{
    [Table("userDrinks")]
    public class UserDrinkEntity
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public int DrinkId { get; set; }
        public int Amount { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}