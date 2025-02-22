namespace ProductRating.Data.Configurations
{
    public record HashServiceOptions
    {
        public int SaltSize { get; init; }
        public int KeySize { get; init; }
        public char Spliter { get; init; }
    }
}