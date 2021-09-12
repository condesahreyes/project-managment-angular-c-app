using System;
using System.Collections.Generic;
using DataAccessInterface;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {

        public T Create(T entity)
        {
            return null;
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public T Get(Guid id)
        {
            return null;
        }

        public IEnumerable<T> GetAll() 
        {
         return null;  
        }

        public void Save()
        {
            
        }

        public void Update(T entity, T entityUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
