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

        public void Delete(string EntityId)
        {
            try
            {
                _context.Tokens.Remove(GetById(EntityId));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during deletion token. Error message: {ex.Message}");
                throw ex;
            }

            Save();
        }

        public void Delete(DbToken Entity)
        {
            try
            {
                _context.Tokens.Remove(Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during deletion token. Error message: {ex.Message}");
                throw ex;
            }

            Save();
        }

        public void DeleteAll()
        {
            if(_context.Tokens.Count() == 0)
            {
                return;
            }

            for (int i = 0; i < _context.Tokens.Count(); i++)
            {
                _context.Tokens.Remove(_context.Tokens.ToList()[i]);
            }
        }

        public IEnumerable<DbToken> GetAll()
        {
            return _context.Tokens;
        }

        public DbToken GetById(string EntityId)
        {
            return _context.Tokens.FirstOrDefault(x => x.Id == EntityId);
        }

        public int GetCount()
        {
            return _context.Tokens.Count();
        }

        public void Insert(DbToken Entity)
        {
            try
            {
                _context.Tokens.Add(Entity);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during insertion token. Error message: {ex.Message}");
                throw ex;
            }

            Save();
        }

        public void InsertRange(IEnumerable<DbToken> Entities)
        {
            try
            {
                _context.Tokens.AddRange(Entities);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during range insertion token. Error message: {ex.Message}");
                throw ex;
            }

            Save();
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
