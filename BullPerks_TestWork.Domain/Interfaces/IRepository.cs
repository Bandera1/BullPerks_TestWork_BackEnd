namespace BullPerks_TestWork.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(string EntityId);
        Task InsertAsync(T Entity);
        Task InsertRangeAsync(IEnumerable<T> Entities);
        Task DeleteAsync(string EntityId);
        Task DeleteAsync(T Entity);
        Task DeleteAllAsync();
        Task<int> GetCountAsync();
        Task SaveAsync();
    }
}
