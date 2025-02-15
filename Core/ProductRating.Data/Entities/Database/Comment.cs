namespace ProductRating.Data.Entities.Database
{
    public class Comment
    {
        public int Id { get; set; }
        public int Review { get; set; }
        public int User { get; set; }
        public bool Vote { get; set; }
        public string Description { get; set; }
    }
}