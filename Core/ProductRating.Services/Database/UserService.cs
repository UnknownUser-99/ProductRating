﻿using ProductRating.Contracts.Database;
using ProductRating.Data.Database;
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
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    User user = new User
                    {
                        Name = name,
                        Phone = phone,
                        Password = password
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    UserHistory userHistory = new UserHistory
                    {
                        User = user.Id,
                        Operation = (int)UserOperationType.Register
                    };

                    _context.UserHistory.Add(userHistory);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return user.Id;
                }
                catch
                {
                    await transaction.RollbackAsync();

                    throw;
                }
            }
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

        public async Task<User> GetUserByPhoneAsync(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Phone == phone);
        }
    }
}