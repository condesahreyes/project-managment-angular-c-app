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

        public T Get(int id)
        {
            return null;
        }

        public IEnumerable<T> GetAll() 
        {
         return null;  
        }

        public T GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public T GetByString(string byGet)
        {
            throw new System.NotImplementedException();
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
