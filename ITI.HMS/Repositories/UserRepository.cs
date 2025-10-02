using ITI.HMS.Models;
using ITI.HMS.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ITI.HMS.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HMSDbContext _dbContext;

        public UserRepository(HMSDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task AddRefreshTokenAsync(int id,RefreshToken refreshToken,bool removeInActiveTokens = false)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if(user != null)
            {
                user.RefreshTokens.Add(refreshToken);
                if (removeInActiveTokens)
                {
                    user.RefreshTokens.RemoveAll(t => !t.IsActive);
                }
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.RefreshTokens.Any(rt => rt.Token == refreshToken && rt.RevokenOn == null && rt.ExpiresOn > DateTime.UtcNow));
        }
    }
}