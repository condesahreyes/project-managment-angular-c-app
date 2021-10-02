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
        private readonly DbContext _context;

        public UserRepository(DbContext context) : base(context)
        {
            this._context = context;
            this._DbSet = context.Set<User>();
        }

        public List<User> GetAll()
        {
            return _DbSet.Include(r => r.Rol).Include(p => p.Projects).ToList();
        }

        public User GetById(Guid id)
        {
            return _DbSet.Include(r => r.Rol).Include(p => p.Projects).First(p => p.Id == id);
        }
    }
}
