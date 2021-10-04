using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IDeveloperLogic
    {
        User Create(User developer);
        User GetByString(string userName);

        Bug UpdateStateToDoneBug(int id);
        Bug UpdateStateToActiveBug(int id);

        List<Bug> GetAllBugs(User developer);

        void AssignDeveloperToProject(Project project, User developer);
        void DeleteDeveloperInProject(Project project, User developer);

        int CountBugDoneByDeveloper(User developer);
    }
}
