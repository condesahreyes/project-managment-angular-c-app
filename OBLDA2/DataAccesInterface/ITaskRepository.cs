using System.Collections.Generic;
using Domain;
using System;

namespace DataAccessInterface
{
    public interface ITaskRepository : IRepository<Task, Guid>
    {
        List<Task> GetAll();
        Task GetById(Guid id);
    }
}
