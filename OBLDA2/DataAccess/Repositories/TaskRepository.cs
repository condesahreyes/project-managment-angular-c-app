using DataAccessInterface;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Repositories
{
    public class TaskRepository : Repository<Task, Guid>, ITaskRepository
    {
        private readonly DbSet<Task> _DbSet;
        private readonly DbContext _context;

        public TaskRepository(DbContext context) : base(context)
        {
            this._context = context;
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
