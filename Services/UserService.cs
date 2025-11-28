using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Data;
using RecipeApp.Models;

namespace RecipeApp.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;
        private readonly PasswordHasher<User> _hasher = new();

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var normalized = email.Trim().ToLowerInvariant();
            return await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == normalized);
        }

        public async Task<User> RegisterAsync(string email, string displayName, string password)
        {
            var existing = await GetByEmailAsync(email);
            if (existing != null)
                throw new InvalidOperationException("Email already registered.");

            var user = new User
            {
                Email = email.Trim(),
                DisplayName = displayName?.Trim() ?? string.Empty
            };

            user.PasswordHash = _hasher.HashPassword(user, password);
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> ValidateCredentialsAsync(string email, string password)
        {
            var user = await GetByEmailAsync(email);
            if (user == null) return null;

            var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
            return result == PasswordVerificationResult.Failed ? null : user;
        }
    }
}

