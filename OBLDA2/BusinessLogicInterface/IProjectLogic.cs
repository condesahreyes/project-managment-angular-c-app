using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace BusinessLogicInterface
{
    public interface IProjectLogic
    {

        Project Create(Project projectToCreate);

        Project Update(Guid id, Project updatedProject);

        void Delete(Guid id);

        void DeleteTester(Project project, User tester);

        void DeleteDeveloper(Project project, User developer);

        void AssignDeveloper(Project project, User developer);

        void AssignTester(Project project, User tester);

        void ImportBugsByProvider(Project project, List<Bug> bugsProject);

        List<Project> GetAll();

        int GetAllFixedBugsByDeveloper(Guid id);

        Project Get(Guid id);
        List<User> GetAllTesters(Project project);
        List<User> GetAllDevelopers(Project project);
    }
}
