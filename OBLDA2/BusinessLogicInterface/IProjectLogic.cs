using System;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Text;
using Domain;
=======
>>>>>>> TesterLogicTest

namespace BusinessLogicInterface
{
    public interface IProjectLogic
<<<<<<< HEAD
    {

         Project Create(Project projectToCreate);
       
         void Update(Guid id, Project updatedProject);

        void Delete(Guid id);

        void DeleteTester(Project project, Guid idTester);

         void DeleteDeveloper(Project project, Guid idDeveloper);

         void AssignDeveloper(Project project, Guid idDeveloper);

         void AssignTester(Project project, Guid idTester);

         void ImportBugsByProvider(Project project, List<Bug> bugsProject);

         List<Project> GetAll();

         int GetAllFixedBugsByDeveloper();

         Project Get(Guid id);
=======

    {
>>>>>>> TesterLogicTest
    }
}
