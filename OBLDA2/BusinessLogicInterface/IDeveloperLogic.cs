using System;
using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IDeveloperLogic
    {
        User Create(User developer);
        User GetByString(string userName);

        Bug UpdateStateToDoneBug(int id, Guid userId);
        Bug UpdateStateToActiveBug(int id, Guid userId);

        List<Bug> GetAllBugs(User developer);

        void AssignDeveloperToProject(Project project, User developer);
        void DeleteDeveloperInProject(Project project, User developer);

        int CountBugDoneByDeveloper(User developer);

        Bug UpdateState(int id, string state, Guid userResolved);
    }
}
