using System;
using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IDeveloperLogic
    {
        List<Bug> GetAllBugs(Guid developer);
        void AssignDeveloperToProject(Guid projectId, Guid developerId);
        void DeleteDeveloperInProject(Guid projectId, Guid developerId);
        int CountBugDoneByDeveloper(Guid developer);
        Bug UpdateState(int id, string state, Guid userResolved);
    }
}
