﻿namespace BullPerks_TestWork.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(string EntityId);
        void Insert(T Entity);
        void InsertRange(IEnumerable<T> Entities);
        void Delete(string EntityId);
        void Delete(T Entity);
        void DeleteAll();
        int GetCount();
        void Save();
    }
}
