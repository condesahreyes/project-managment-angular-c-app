using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;

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

        public Repository() { }

        public T Create(T entity)
        {
            T entitiyToReturn = _DbSet.Add(entity).Entity;
            Save();

            return entitiyToReturn;
        }

        public void Delete(K id)
        {
            _DbSet.Remove(_DbSet.Find(id));
            Save();
        }

        public T Get(K id)
        {
            return _DbSet.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T Update(K id, T entity)
        {
            T entitiyToReturn = _DbSet.Update(entity).Entity;
            Save();

            return entitiyToReturn;
        }

        public List<T> GetAllGeneric()
        {
            return _DbSet.ToList();
        }
    }
}
