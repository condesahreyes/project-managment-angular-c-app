using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;

namespace DataAccess.Repositories
{
    public class TaskRepository : Repository<Task, Guid>, ITaskRepository
    {
        private readonly DbSet<Task> _DbSet;

        public TaskRepository(DbContext context) : base(context)
        {
            this._DbSet = context.Set<Task>();
        }

        public List<Task> GetAll()
        {
            return _DbSet.Include(t => t.Project).ToList();
        }

        public Task GetById(Guid id)
        {
            return _DbSet.Include(t => t.Project).First(t => t.Id == id);
        }
    }
}
