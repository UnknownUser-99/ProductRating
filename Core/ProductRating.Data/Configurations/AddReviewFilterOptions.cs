namespace ProductRating.Data.Configurations
{
    public record AddReviewFilterOptions
    {
        public int RatingMinValue { get; init; }
        public int RatingMaxValue { get; init; }
        public int DescriptionMaxLength { get; init; }
    }
}