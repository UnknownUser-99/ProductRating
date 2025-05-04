namespace ProductRating.Data.Models
{
    public class ReviewModel
    {
        public string User { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
        public DateOnly Date { get; set; }
    }
}