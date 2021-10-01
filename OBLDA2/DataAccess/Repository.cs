using System;
using System.Collections.Generic;
using DataAccessInterface;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class Repository<T, K> : IRepository<T, K> where T : class
    {
        private readonly DbSet<T> _DbSet;
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            this._context = context;
            this._DbSet = context.Set<T>();

        }

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

        public List<T> GetAll()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public T Update(K id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
