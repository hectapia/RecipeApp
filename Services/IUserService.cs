using RecipeApp.Models;

namespace RecipeApp.Services
{
    public interface IUserService
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> RegisterAsync(string email, string displayName, string password);
        Task<User?> ValidateCredentialsAsync(string email, string password);
    }
}
