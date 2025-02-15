namespace ProductRating.Data.Entities.Database
{
    public class RecognitionHistory
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public int User { get; set; }
        public string Confidence { get; set; }
        public DateTime Date { get; set; }
    }
}