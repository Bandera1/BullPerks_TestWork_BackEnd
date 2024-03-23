using BullPerks_TestWork.Controllers;
using BullPerks_TestWork.DB;
using BullPerks_TestWork.DB.Models;
using BullPerks_TestWork.Repositories.Interfaces;

namespace BullPerks_TestWork.Repositories.Implementations
{
    public class DbTokenRepository : IRepository<DbToken>
    {
        private readonly EFDbContext _context;
        private readonly ILogger<WeatherForecastController> _logger;

        public DbTokenRepository(EFDbContext context, ILogger<WeatherForecastController> logger)
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

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
