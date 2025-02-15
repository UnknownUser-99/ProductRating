namespace ProductRating.Data.Entities.Database
{
    public class CommentHistory
    {
        public int Id { get; set; }
        public int Comment { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }
    }
}