namespace ProductRating.Data.Entities.Database
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public float OverallRating { get; set; }
        public float YearlyRating { get; set; }
        public float MonthlyRating { get; set; }
    }
}