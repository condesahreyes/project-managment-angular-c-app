using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);

        T Get();
        IEnumerable<T> GetAll();
        void Save();
        void Delete(T entity);
        void Update(T entity, T entityUpdate);

    }
}
