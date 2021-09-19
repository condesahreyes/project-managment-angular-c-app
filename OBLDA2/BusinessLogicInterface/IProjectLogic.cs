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

        void DeleteTester(Project project, Guid idTester);

        void DeleteDeveloper(Project project, Guid idDeveloper);

        void AssignDeveloper(Project project, Guid idDeveloper);

        void AssignTester(Project project, Guid idTester);

        void ImportBugsByProvider(Project project, List<Bug> bugsProject);

        List<Project> GetAll();

        int GetAllFixedBugsByDeveloper(Guid id);

        Project Get(Guid id);
        List<User> GetAllTesters(Project project);
        List<User> GetAllDevelopers(Project project);
    }
}
