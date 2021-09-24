using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface IRepository<T, K> where T : class
    {
        T Create(T entity);
        T Get(K id);
        List<T> GetAll();
        void Save();
        void Delete(K id);
        T Update(K id, T entity);
    }
}
