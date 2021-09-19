using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System;
using Domain;

namespace BusinessLogic.UserRol
{
    public class ProjectLogic : IProjectLogic
    {
        private IRepository<Project, Guid> projectDa;

        private IProjectLogic projectLogic;
        private IBugLogic bugLogic;

        public ProjectLogic(IRepository<Project, Guid> ProjectDa, 
            IProjectLogic projectLogic, IBugLogic bugLogic)
        {
            this.projectDa = ProjectDa;
            this.projectLogic = projectLogic;
            this.bugLogic = bugLogic;
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

        public List<Bug> GetAllBugByProject(Project project)
        {
            List<Bug> bugs = (List<Bug>)bugLogic.GetAll();
            List<Bug> bugsByProject = new List<Bug>();

            foreach (Bug bug in bugs)
                if (bug.Project.Id == project.Id)
                    bugsByProject.Add(bug);

            return bugsByProject;
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
