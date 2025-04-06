namespace ProductRating.Data.Database
{
    public class RecognitionHistory
    {
        public int Id { get; set; }
        public int Product { get; set; }
        public int User { get; set; }
        public int Confidence { get; set; }
        public DateTime Date { get; set; }
    }
}