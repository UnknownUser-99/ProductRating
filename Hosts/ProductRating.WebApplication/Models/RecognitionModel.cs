namespace ProductRating.WebApplication.Models
{
    public class RecognitionModel
    {
        public string Product { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public string OverallRating { get; set; }
        public string YearlyRating { get; set; }
        public string MonthlyRating { get; set; }
        public string Confidence { get; set; }
    }
}