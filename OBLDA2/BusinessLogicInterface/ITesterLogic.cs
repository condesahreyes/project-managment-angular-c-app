using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface ITesterLogic
    {
        User Get(Guid id);
        List<Project> GetProjectsByTester(Guid id);
        List<User> GetAll();
        List<Bug> GetAllBugs(Guid testerId);
        void AssignTesterToProject(Guid projectId, Guid testerId);
        void DeleteTesterInProject(Guid projectId, Guid testerId);
    }
}
