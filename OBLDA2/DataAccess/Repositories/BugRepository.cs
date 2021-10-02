using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;

namespace DataAccess.Repositories
{
    public class BugRepository : Repository<Bug, int>, IBugRepository
    {
        private readonly DbSet<Bug> _DbSet;
        private readonly DbContext _context;

        public BugRepository(DbContext context) : base(context)
        {
            this._context = context;
            this._DbSet = context.Set<Bug>();
        }

        public List<Bug> GetAll()
        {
            throw new NotImplementedException();
        }

        public Bug GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
