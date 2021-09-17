using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
<<<<<<< HEAD
    public interface IRepository<T,K> where T : class
=======
    public interface IRepository<T, K> where T : class
>>>>>>> TesterLogicTest
    {
        T Create(T entity);
        T Get(K id);
        IEnumerable<T> GetAll();
        void Save();
        void Delete(K id);
        void Update(K id, T entity);
    }
}
