using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MacroTrackApi.Models.Entities
{
    [Table("drinks")]
    public class DrinkEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}