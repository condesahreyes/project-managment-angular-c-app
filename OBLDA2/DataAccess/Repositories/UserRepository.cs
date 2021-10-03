using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;

namespace DataAccess.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        private readonly DbSet<User> _DbSet;

        public UserRepository(DbContext context) : base(context)
        {
            this._DbSet = context.Set<User>();
        }
        public UserRepository()
        {
        }

        public List<User> GetAll()
        {
            return _DbSet.Include(r => r.Rol).Include(p => p.Projects).ToList();
        }

        public User GetById(Guid id)
        {
            return _DbSet.Include(r => r.Rol).Include(p => p.Projects).First(p => p.Id == id);
        }

        public void UpdateUser(User user)
        {
            User entitiyToReturn = _DbSet.Update(user).Entity;
            Save();
        }
    }
}
