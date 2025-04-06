namespace ProductRating.Data.Database
{
    public class ReviewHistory
    {
        public int Id { get; set; }
        public int Review { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }
    }
}