namespace ProductRating.Data.WebAPI.Results
{
    public record ErrorsResult
    {
        public List<string> Errors { get; init; }
    }
}