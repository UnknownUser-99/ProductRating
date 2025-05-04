namespace ProductRating.Data.Configurations
{
    public record RegistrationFilterOptions
    {
        public int PhoneMinLength { get; init; }
        public int PhoneMaxLength { get; init; }
        public string PhoneRegularExpression { get; init; }
        public int NameMinLength { get; init; }
        public int NameMaxLength { get; init; }
        public int PasswordMinLength { get; init; }
        public int PasswordMaxLength { get; init; }
    }
}