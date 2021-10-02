using System.Collections.Generic;
using Domain;
using System;

namespace DataAccessInterface
{
    public interface IProjectRepository : IRepository<Project, Guid>
    {
        List<Project> GetAll();
        Project GetById(Guid id);
    }
}
