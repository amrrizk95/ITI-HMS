using ITI.HMS.Models;

namespace ITI.HMS.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task AddRefreshTokenAsync(int id, RefreshToken refreshToken,bool removeInActiveTokens = false);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    }
}