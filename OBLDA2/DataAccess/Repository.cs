﻿using System;
using System.Collections.Generic;
using DataAccessInterface;

namespace DataAccess
{
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        public T Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(K id)
        {
            throw new NotImplementedException();
        }

        public T Get(K id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(K id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
