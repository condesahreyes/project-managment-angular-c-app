using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;

namespace DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project, Guid>, IProjectRepository
    {
        private readonly DbSet<Project> _DbSet;
        private readonly DbContext _context;

        public ProjectRepository(DbContext context) : base(context)
        {
            this._context = context;
            this._DbSet = context.Set<Project>();
        }

        public List<Project> GetAll()
        {
            return _DbSet.Include(u => u.Users)
                .Include(b => b.Bugs)
                .ToList();
        }

        public Project GetById(Guid id)
        {
            return _DbSet.Include(u => u.Users)
                .Include(b => b.Bugs).First(b => b.Id == id);
        }
    }
}
