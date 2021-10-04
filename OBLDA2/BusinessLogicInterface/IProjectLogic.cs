using System.Collections.Generic;
using Domain;
using System;

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

        Project Get(Guid id);

        List<Project> GetAll();
        List<User> GetAllTesters(Project project);
        List<User> GetAllDevelopers(Project project);
        List<Bug> GetAllBugByProject(Project project);
    }
}
