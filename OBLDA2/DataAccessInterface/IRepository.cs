using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface IRepository<T> where T : class
    {
        T Create(T entity);
        void Delete(T entity);

        T Get(Guid id);
        IEnumerable<T> GetAll();
        void Save();
       
    }
}
