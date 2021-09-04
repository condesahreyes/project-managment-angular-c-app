using System;
using System.Collections.Generic;
using DataAccessInterface;

namespace DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {



        public void Create(T entity)
        {
            
        }
             
        public T Get(int id)
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
    }
}
