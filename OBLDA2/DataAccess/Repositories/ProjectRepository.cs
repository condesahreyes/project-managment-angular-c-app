using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
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
            throw new NotImplementedException();
        }

        public Project GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
