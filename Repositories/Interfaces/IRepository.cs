namespace BullPerks_TestWork.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string EntityId);
        void Insert(T Entity);
        void Delete(string EntityId);
        void Delete(T Entity);
        int GetCount();
        void Save();
    }
}
