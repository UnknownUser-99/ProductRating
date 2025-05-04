namespace ProductRating.Data.Configurations
{
    public record AuthControllerOptions
    {
        public int LoginMinLength { get; init; }
        public int LoginMaxLength { get; init; }
        public int PasswordMinLength { get; init; }
        public int PasswordMaxLength { get; init; }
        public int EmailMinLength { get; init; }
        public int EmailMaxLength { get; init; }
    }
}