using ProductRating.Contracts.Database;
using ProductRating.Data.Entities.Database;
using Microsoft.EntityFrameworkCore;

namespace ProductRating.Services.Database
{
    public class UserService : IUserService
    {
        private readonly PRDbContext _context;

        public UserService(PRDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddUserAsync(int phone, string name, string password, string email = null)
        {
            User user = new User
            {
                Name = name,
                Phone = phone,
                Email = email,
                Password = password
            };

            UserHistory userHistory = new UserHistory
            {
                User = user.Id,
                Operation = UserOperationType.Register.ToString(),
                Date = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.UserHistory.Add(userHistory);

            await _context.SaveChangesAsync();

            return user.Id;
        }
    }
}