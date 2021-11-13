using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface ITesterLogic
    {
        User Get(Guid id);

        List<User> GetAll();
        List<Bug> GetAllBugs(Guid testerId);
        List<Project> GetAllProjects(Guid testerId);
        List<Task> GetAllTask(Guid testerId);

        void AssignTesterToProject(Guid projectId, Guid testerId);
        void DeleteTesterInProject(Guid projectId, Guid testerId);
    }
}
