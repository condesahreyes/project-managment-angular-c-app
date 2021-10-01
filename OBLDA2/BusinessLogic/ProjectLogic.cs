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
        private IBugLogic bugLogic;

        public ProjectLogic() { }

        public ProjectLogic(IRepository<Project, Guid> ProjectDa, IBugLogic bugLogic)
        {
            this.projectDa = ProjectDa;
            this.bugLogic = bugLogic;
        }

        public Project Create(Project projectToCreate)
        {
            Project.ValidateName(projectToCreate.Name);

            return projectDa.Create(projectToCreate);
        }

        public Project Get(Guid id)
        {
            Project projcet = projectDa.Get(id);

            if (projcet != null)
            {
                return projcet;
            }
            else
            {
                throw new Exception("Project does not exist");
            }
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

        public void DeleteTester(Project oneProject, User tester)
        {
            Project project = projectDa.Get(oneProject.Id);

              if (project.Users.Contains(tester))
              {
                project.Users.Remove(tester);
              }
        }

        public void DeleteDeveloper(Project oneProject, User developer)
        {
            Project project = projectDa.Get(oneProject.Id);

            if (project.Users.Contains(developer))
            {
                project.Users.Remove(developer);
            }
        }

        public void AssignDeveloper(Project oneProject, User developer)
        {
            Project project = projectDa.Get(oneProject.Id);

            if (!project.Users.Contains(developer))
            {
                project.Users.Add(developer);
            }
        }

        public void AssignTester(Project oneProject, User tester)
        {
            Project project = projectDa.Get(oneProject.Id);

            if (!project.Users.Contains(tester))
            {
                project.Users.Add(tester);
            }
        }

        public List<Project> GetAll()
        {
            return projectDa.GetAllGeneric();
        }

        public List<User> GetAllTesters(Project oneProject)
        {
            List<User> testers = new List<User>();

            Project project = projectDa.Get(oneProject.Id);

            foreach (User user in project.Users)
            {
                if (user.Rol.Name == Rol.tester)
                    testers.Add(user);
            }

            return testers;
        }

        public List<User> GetAllDevelopers(Project oneProject)
        {
            List<User> developers = new List<User>();

            Project project = projectDa.Get(oneProject.Id);

            foreach (User user in project.Users)
            {
                if (user.Rol.Name == Rol.developer)
                    developers.Add(user);
            }

            return developers;
        }

        public List<Bug> GetAllBugByProject(Project project)
        {
            List<Bug> bugs = bugLogic.GetAll();  // ESTO PODRIA SER GER(PROJECT.ID) Y DEVOLVE PROJECT.BUGS
            List<Bug> bugsByProject = new List<Bug>();

            foreach (Bug bug in bugs)
                if (bug.Project.Id == project.Id)
                    bugsByProject.Add(bug);

            return bugsByProject;
        }

    }
}
