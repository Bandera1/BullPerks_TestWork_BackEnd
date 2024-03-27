using BullPerks_TestWork.Api.Repositories.Interfaces;
using BullPerks_TestWork.Domain.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BullPerks_TestWork.DAL.Repositories
{
    public class DbTokenRepository : IRepository<DbToken>
    {
        private readonly EFDbContext _context;
        private readonly ILogger<DbTokenRepository> _logger;

        public DbTokenRepository(EFDbContext context, ILogger<DbTokenRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task DeleteAsync(string EntityId)
        {
            try
            {
                await _context.Tokens.Where(x => x.Id == EntityId).ExecuteDeleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during deletion token. Error message: {ex.Message}");
                throw ex;
            }
        }

        public async Task DeleteAsync(DbToken Entity)
        {
            try
            {
               await _context.Tokens.Where(x => x.Id == Entity.Id).ExecuteDeleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during deletion token. Error message: {ex.Message}");
                throw ex;
            }
        }

        public async Task DeleteAllAsync()
        {
            if(_context.Tokens.Count() == 0)
            {
                return;
            }

            await _context.Tokens.Where(x => 1 == 1).ExecuteDeleteAsync();
        }

        public async Task<IEnumerable<DbToken>> GetAllAsync()
        {
            return await _context.Tokens.ToListAsync(); // TODO: DO NOT MATERIALIZE
        }

        public async Task<DbToken> GetByIdAsync(string EntityId)
        {
            return await _context.Tokens.FirstOrDefaultAsync(x => x.Id == EntityId);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Tokens.CountAsync();
        }

        public async Task InsertAsync(DbToken Entity)
        {
            try
            {
                await _context.Tokens.AddAsync(Entity);
                SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during insertion token. Error message: {ex.Message}");
                throw ex;
            } 
        }

        public async Task InsertRangeAsync(IEnumerable<DbToken> Entities)
        {
            try
            {
                await _context.Tokens.AddRangeAsync(Entities);
                SaveAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during range insertion token. Error message: {ex.Message}");
                throw ex;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
