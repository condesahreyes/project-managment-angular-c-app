using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessInterface
{
    public interface IRepository<T,K> where T : class
    {
        T Create(T entity);

<<<<<<< HEAD
        T GetById(int id);
        T GetByString(string byGet);
=======
        T Get(K id);
>>>>>>> administratorLogicTest
        IEnumerable<T> GetAll();
        void Save();
        void Delete(K id);
        void Update(K id, T entity);
    }
}
