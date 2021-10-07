using System.Collections.Generic;

namespace DataAccessInterface
{
    public interface IRepository<T, K> where T : class
    {
        T Create(T entity);
        T Get(K id);
        List<T> GetAllGeneric();
        void Save();
        void Delete(K id);
        
    }
}
