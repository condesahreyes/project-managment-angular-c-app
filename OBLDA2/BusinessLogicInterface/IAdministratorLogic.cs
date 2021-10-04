using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface IAdministratorLogic
    {
        User Create(User adminToCreate);
        User Get(Guid id);

        List<User> GetAll();
        List<User> GetAllTesters(Project project);
        List<User> GetAllDevelopers(Project project);

        Project CreteProject(Project projectToCreate);
        Project UpdateProject(Guid id, Project updatedProject);

        Bug CreateBug(Bug bugToCreate);
        Bug UpdateBug(int id, Bug updatedBug);

        void DeleteProject(Guid id);
        void DeleteBug(int id);
        void AssignDeveloperByProject(Project project, User developer);
        void AssignTesterByProject(Project project, User tester);
        void DeleteTesterByProject(Project project, User tester);
        void DeleteDeveloperByProject(Project project, User developer);

        int GetFixedBugsByDeveloper(Guid id);
        int GetTotalBugByAllProject();
    }
}
