using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic.UserRol
{
    public class ProjectLogic : IProjectLogic
    {
        private IRepository<Project, Guid> projectDa;

        public ProjectLogic(IRepository<Project, Guid> ProjectDa)
        {
            this.projectDa = ProjectDa;
        }

        public Project Create(Project projectToCreate)
        {
            throw new NotImplementedException();
        }

        public void Update(Guid id, Project updatedProject)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTester(Project project, Guid idTester)
        {
            throw new NotImplementedException();
        }

        public void DeleteDeveloper(Project project, Guid idDeveloper)
        {
            throw new NotImplementedException();
        }

        public void AssignDeveloper(Project project, Guid idDeveloper)
        {
            throw new NotImplementedException();
        }

        public void AssignTester(Project project, Guid idTester)
        {
            throw new NotImplementedException();
        }

        public void ImportBugsByProvider(Project project, List<Bug> bugsProject)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetAllFixedBugsByDeveloper()
        {
            throw new NotImplementedException();
        }

        public Project Get(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
