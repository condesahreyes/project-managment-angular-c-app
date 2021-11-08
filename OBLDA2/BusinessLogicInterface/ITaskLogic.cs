using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface ITaskLogic
    {
        Task Create(Task task);
        List<Task> GetAll();
        List<Task> GetAllByProject(Guid idProject);
    }
}
