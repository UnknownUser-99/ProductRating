namespace ProductRating.Contracts.Database
{
    public interface IUserService
    {
        Task<int> AddUserAsync(int phone, string name, string password);
    }
}