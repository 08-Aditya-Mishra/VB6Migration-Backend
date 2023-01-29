using MigrationTask.Models;
using Microsoft.EntityFrameworkCore;
using MigrationTask.Services.RefreshTokenRepositories;

namespace MigrationTask.Services.RefreshTokenRepositories
{
    public class DatabaseRefreshTokenRepository : IRefreshTokenRepository
    {
        // private readonly ApplicationDbContext _Context;
        // public DatabaseRefreshTokenRepository(ApplicationDbContext context)
        // {
        //     _Context = context;
        // }
        private readonly List<RefreshToken> _refreshTokens = new List<RefreshToken>();
        public Task Create(RefreshToken refreshToken)
        {
            refreshToken.Id = Guid.NewGuid();
            _refreshTokens.Add(refreshToken);
            return Task.CompletedTask;
        }

        public Task Delete(Guid id)
        {
            _refreshTokens.RemoveAll(r => r.Id == id);
            return Task.CompletedTask;
        }

        public Task DeleteAll(int adminId)
        {
            _refreshTokens.RemoveAll(r => r.AdminId == adminId);
            return Task.CompletedTask;
        }

        public Task<RefreshToken> GetByToken(string token)
        {
            RefreshToken refreshToken = _refreshTokens.FirstOrDefault(r => r.Token == token);
            return Task.FromResult(refreshToken);
        }
    }
}
