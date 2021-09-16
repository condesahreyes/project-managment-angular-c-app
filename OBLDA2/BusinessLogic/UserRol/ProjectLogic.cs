using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicInterface;
using Domain;

namespace BusinessLogic.UserRol
{
    public class ProjectLogic : IProjectLogic
    {
        internal Project Create(Project projectToCreate)
        {
            throw new NotImplementedException();
        }

        internal void Update(Guid id, Project updatedProject)
        {
            throw new NotImplementedException();
        }

        internal void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        internal void DeleteTester(Project project, Guid idTester)
        {
            throw new NotImplementedException();
        }

        internal void DeleteDeveloper(Project project, Guid idDeveloper)
        {
            throw new NotImplementedException();
        }

        internal void AssignDeveloper(Project project, Guid idDeveloper)
        {
            throw new NotImplementedException();
        }

        internal void AssignTester(Project project, Guid idTester)
        {
            throw new NotImplementedException();
        }

        internal void ImportBugsByProvider(Project project, List<Bug> bugsProject)
        {
            throw new NotImplementedException();
        }

        internal List<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        internal int GetAllFixedBugsByDeveloper()
        {
            throw new NotImplementedException();
        }
    }
}
