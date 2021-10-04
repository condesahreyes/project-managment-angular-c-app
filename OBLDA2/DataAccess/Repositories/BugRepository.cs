using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;
using Exceptions;

namespace DataAccess.Repositories
{
    public class BugRepository : Repository<Bug, int>, IBugRepository
    {
        private const string noExistBug = "No exist bug with id ";
        private readonly DbSet<Bug> _DbSet;
        private readonly DbContext _context;

        public BugRepository() { }

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
            bugSaved.SolvedBy = bugUpdate.SolvedBy;

            if (bugUpdate.Name != null)
                bugSaved.Name = bugUpdate.Name;
            if (bugUpdate.Project != null)
                bugSaved.Project = bugUpdate.Project;
            if (bugUpdate.State != null)
                bugSaved.State = bugUpdate.State;
            if (bugUpdate.Version != null)
                bugSaved.Version = bugUpdate.Version;

            Bug entitiyToReturn = _DbSet.Update(bugSaved).Entity;
            Save();

            return entitiyToReturn;
        }
    }
}
