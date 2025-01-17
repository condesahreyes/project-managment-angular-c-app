﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Exceptions;
using Domain;
using System;

namespace DataAccess.Repositories
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        private const string notExistUser = "Not exist user with this id";
        private readonly DbSet<User> _DbSet;

        public UserRepository(DbContext context) : base(context)
        {
            this._DbSet = context.Set<User>();
        }

        public UserRepository() { }

        public List<User> GetAll()
        {
            return _DbSet.Include(r => r.Rol).ToList();
        }

        public User GetById(Guid id)
        {
            try {
                return _DbSet.Include(r => r.Rol)
                    .First(p => p.Id == id);
            }
            catch (Exception)
            {
                throw new NoObjectException(notExistUser);
            }
        }

        public void UpdateUser(User user)
        {
            User entitiyToReturn = _DbSet.Update(user).Entity;
            Save();
        }
    }
}
