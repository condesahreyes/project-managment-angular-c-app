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
        private IProjectLogic projectLogic;

        public ProjectLogic(IRepository<Project, Guid> ProjectDa, IProjectLogic projectLogic)
        {
            this.projectDa = ProjectDa;
            this.projectLogic = projectLogic;
        }

        public ProjectLogic()
        {
        }

        public Project Create(Project projectToCreate)
        {
            Project.ValidateName(projectToCreate.Name);

            return projectDa.Create(projectToCreate);

        }

        public Project Update(Guid id, Project updatedProject)
        {
            Project.ValidateName(updatedProject.Name);
            
            return projectDa.Update(id, updatedProject);
        }

        public void Delete(Guid id)
        {
            projectDa.Delete(id);
        }

        public void DeleteTester(Project project, User tester)
        {
            var proj = projectDa.Get(project.Id);

              if (proj.testers.Contains(tester))
              {
                 proj.testers.Remove(tester);
              }
            
        }

        public void DeleteDeveloper(Project project, User developer)
        {
            var proj = projectDa.Get(project.Id);

            if (proj.developers.Contains(developer))
            {
                proj.developers.Remove(developer);
            }

        }

        public void AssignDeveloper(Project project, User developer)
        {
            var proj = projectDa.Get(project.Id);

            if (!proj.developers.Contains(developer))
            {
                proj.developers.Add(developer);
            }

        }

        public void AssignTester(Project project, User tester)
        {
            var proj = projectDa.Get(project.Id);

            if (!proj.testers.Contains(tester))
            {
                proj.testers.Add(tester);
            }
        }

        public void ImportBugsByProvider(Project project, List<Bug> bugsProject)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetAll()
        {
            return projectDa.GetAll();
        }

        public List<User> GetAllTesters(Project project)
        {
            var proj = projectDa.Get(project.Id);

            return proj.testers;
        }

        public List<User> GetAllDevelopers(Project project)
        {
            var proj = projectDa.Get(project.Id);

            return proj.developers;
        }

        public int GetAllFixedBugsByDeveloper(Guid id)
        {
            throw new NotImplementedException();
        }

        public Project Get(Guid id)
        {
            var projcet = projectDa.Get(id);
            if (projcet != null)
            {
                return projcet;
            }
            else
            {
                throw new Exception("Project does not exist");
            }
        }
    }
}
