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
            return _DbSet.Include(p => p.Project).Include(s => s.State).Include(u => u.SolvedBy)
            .ToList();
        }

        public Bug GetById(int id)
        {
            return _DbSet.Include(p => p.Project).Include(s => s.State)
                .Include(u => u.SolvedBy).First(b => b.Id == id);
        }
    }
}
