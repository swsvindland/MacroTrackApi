namespace MacroTrackApi.Models.DTOs
{
    public class FoodDTO
    {
        public long Id { get; set; }
        public int Servings { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public int ServingSize { get; set; }
        public int Calories { get; set; }
        public int Protein { get; set; }
        public int Fat { get; set; }
        public int Carbs { get; set; }
        public int Alcohol { get; set; }
        public int Fiber { get; set; }
    }
}