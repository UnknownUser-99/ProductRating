namespace ProductRating.Contracts.Authorization
{
    public interface IHashService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashPassword);
    }
}