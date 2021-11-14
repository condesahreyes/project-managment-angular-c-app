using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Exceptions;
using Domain;
using System;

namespace DataAccess.Repositories
{
    public class BugRepository : Repository<Bug, int>, IBugRepository
    {
        private readonly DbSet<Bug> _DbSet;
        private readonly DbSet<State> _DbSetState;
        private readonly DbSet<User> _DbSetUser;

        public BugRepository() { }

        public BugRepository(DbContext context) : base(context)
        {
            this._DbSet = context.Set<Bug>();
            this._DbSetState = context.Set<State>();
            this._DbSetUser = context.Set<User>();
        }

        public List<Bug> GetAll()
        {
            return _DbSet.Include(p => p.Project)
                .Include(s => s.State).Include(u => u.SolvedBy)
                .ToList();
        }

        public Bug GetById(int id)
        {
            try { 
                return _DbSet.Include(p => p.Project).Include(s => s.State)
                    .Include(u => u.SolvedBy).First(b => b.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Bug Update(int id, Bug bugUpdate)
        {
            Bug bugSaved = GetById(id);

            if (bugUpdate.Name != null)
                bugSaved.Name = bugUpdate.Name;
            if (bugUpdate.Project != null)
                bugSaved.Project = bugUpdate.Project;
            if (bugUpdate.State != null)
                bugSaved.State = _DbSetState.Find(bugUpdate.State.Name);
            if (bugUpdate.Version != null)
                bugSaved.Version = bugUpdate.Version;
            if (bugUpdate.Duration != null)
                bugSaved.Duration = bugUpdate.Duration;
            if (bugUpdate.Domain != null)
                bugSaved.Domain = bugUpdate.Domain;
            if (bugUpdate.SolvedBy != null)
                bugSaved.SolvedBy = _DbSetUser.Find(bugUpdate.SolvedBy.Id);
            else
                bugSaved.SolvedBy = null;

            Bug entitiyToReturn = _DbSet.Update(bugSaved).Entity;
            Save();

            return entitiyToReturn;
        }
    }
}
