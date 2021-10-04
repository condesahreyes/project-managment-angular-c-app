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

        public Project Update(Guid id, Project projectUpdate)
        {
            Project projectSaved = GetById(id);

            if (projectUpdate.Name != null)
                projectSaved.Name = projectUpdate.Name;
            if (projectUpdate.TotalBugs != null)
                projectSaved.TotalBugs = projectUpdate.TotalBugs;
            if (projectUpdate.Bugs != null)
                projectSaved.Bugs = projectUpdate.Bugs;
            if (projectUpdate.Users != null)
                projectSaved.Users = projectUpdate.Users;

            Project projectToReturn = _DbSet.Update(projectSaved).Entity;
            Save();

            return projectToReturn;
        }
    }
}
