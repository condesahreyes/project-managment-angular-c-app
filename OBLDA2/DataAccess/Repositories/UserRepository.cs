using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
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
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
