using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Exceptions;
using Domain;
using System;

namespace DataAccess.Repositories
{
    public class ProjectRepository : Repository<Project, Guid>, IProjectRepository
    {
        private const string notExistProject = "Not exist project with this name or id";

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
                .Include(b => b.Bugs).ThenInclude(b => b.State)
                .Include(b => b.Bugs).ThenInclude(b=>b.SolvedBy)
                .ToList();
        }

        public Project GetById(Guid id)
        {
            try { 
                return _DbSet.Include(u => u.Users)
                    .Include(b => b.Bugs).ThenInclude(b => b.State)
                    .Include(b => b.Bugs).ThenInclude(b => b.SolvedBy)
                    .First(b => b.Id == id);
            }
            catch
            {
                throw new NoObjectException(notExistProject);
            }
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
