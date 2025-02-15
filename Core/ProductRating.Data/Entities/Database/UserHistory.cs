namespace ProductRating.Data.Entities.Database
{
    public class UserHistory
    {
        public int Id { get; set; }
        public int User { get; set; }
        public string Operation { get; set; }
        public DateTime Date { get; set; }
    }
}