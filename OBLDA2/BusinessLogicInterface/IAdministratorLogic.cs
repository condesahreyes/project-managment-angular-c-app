using System;
using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IAdministratorLogic
    {

        User Create(User adminToCreate);
        IEnumerable<User> GetAll();
        User Get(Guid id);
        Project CreteProject(Project projectToCreate);
        Project UpdateProject(Guid id, Project updatedProject);
        void DeleteProject(Guid id);
        void DeleteTesterByProject(Project project, User tester);
        void DeleteDeveloperByProject(Project project, User developer);
        Bug CreateBug(Bug bugToCreate);
        Bug UpdateBug(int id, Bug updatedBug);
        void DeleteBug(int id);
        void AssignDeveloperByProject(Project project, User developer);
        void AssignTesterByProject(Project project, User tester);
        void ImportBugsByProjectByProvider(Project project, List<Bug> bugsProject);
        int GetFixedBugsByDeveloper(Guid id);
        List<User> GetAllTesters(Project project);
        List<User> GetAllDevelopers(Project project);
        int GetTotalBugByAllProject();


    }
}
