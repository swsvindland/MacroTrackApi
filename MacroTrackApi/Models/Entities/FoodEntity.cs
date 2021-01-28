using System;
using System.ComponentModel.DataAnnotations.Schema;
using MacroTrackApi.Models.Types;

namespace MacroTrackApi.Models.Entities
{
    [Table("foods")]
    public class FoodEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int ServingSize { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Alcohol { get; set; }
        public int Fiber { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
    }
}