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

        public async Task<int> AddUserAsync(string phone, string name, string password)
        {
            User user = new User
            {
                Name = name,
                Phone = phone,
                Password = password
            };

            UserHistory userHistory = new UserHistory
            {
                User = user.Id,
                Operation = (int)UserOperationType.Register
            };

            _context.Users.Add(user);
            _context.UserHistory.Add(userHistory);

            await _context.SaveChangesAsync();

            return user.Id;
        }

        public async Task<bool> UpdateUserRoleAsync(int id, UserRoleType role)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            user.Role = (int)role;

            UserHistory userHistory = new UserHistory
            {
                User = user.Id,
                Operation = (int)UserOperationType.ChangeRole
            };

            _context.Users.Update(user);
            _context.UserHistory.Add(userHistory);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}