using ProductRating.Data.Database;

namespace ProductRating.Contracts.Database
{
    public interface IUserService
    {
        Task<int> AddUserAsync(string phone, string name, string password);
        Task<User> GetUserByPhone(string phone);
    }
}