namespace ProductRating.Data.Database
{
    public class Review
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public int User { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }
    }
}