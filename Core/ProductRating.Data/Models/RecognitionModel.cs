namespace ProductRating.Data.Models
{
    public class RecognitionModel
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }
        public string Image { get; set; }
        public decimal OverallRating { get; set; }
        public decimal YearlyRating { get; set; }
        public decimal MonthlyRating { get; set; }
        public string Confidence { get; set; }
        public string ConfidenceColor { get; set; }
        public ReviewModel[] Reviews { get; set; }
    }
}