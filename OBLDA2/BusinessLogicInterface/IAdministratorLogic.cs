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
        void DeleteTesterByProject(Project project, Guid idTester);
        void DeleteDeveloperByProject(Project project, Guid idDeveloper);
        void CreteBug(Bug bugToCreate);
        void UpdateBug(int id, Bug updatedBug);
        void DeleteBug(int id);
        void AssignDeveloperByProject(Project project, Guid idDeveloper);
        void AssignTesterByProject(Project project, Guid idTester);
        void ImportBugsByProjectByProvider(Project project, List<Bug> bugsProject);
        List<Project> GetTotalBugByAllProject();
        int GetFixedBugsByDeveloper();


    }
}
