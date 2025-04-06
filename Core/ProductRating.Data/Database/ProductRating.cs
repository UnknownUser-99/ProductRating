namespace ProductRating.Data.Database
{
    public class ProductRating
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public decimal OverallRating { get; set; }
        public decimal YearlyRating { get; set; }
        public decimal MonthlyRating { get; set; }
    }
}