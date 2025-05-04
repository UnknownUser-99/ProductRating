namespace ProductRating.Data.Models
{
    public class AddReviewModel
    {
        public int Product { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}