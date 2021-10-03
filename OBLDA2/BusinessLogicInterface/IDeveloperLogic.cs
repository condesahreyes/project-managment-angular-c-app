using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IDeveloperLogic
    {
        User Create(User developer);
        List<Bug> GetAllBugs(User developer);
        User GetByString(string userName);
        Bug UpdateStateToDoneBug(int id);
        Bug UpdateStateToActiveBug(int id);
        void AssignDeveloperToProject(Project project, User developer);
        void DeleteDeveloperInProject(Project project, User developer);
        int CountBugDoneByDeveloper(User developer);
    }
}
