using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System;
using Domain;

namespace BusinessLogic
{
    public class ProjectLogic : IProjectLogic
    {
        private IProjectRepository projectRepository;

        public ProjectLogic() { }

        public ProjectLogic(IProjectRepository ProjectDa)
        {
            this.projectRepository = ProjectDa;
        }

        public Project Create(Project projectToCreate)
        {
            Project.ValidateName(projectToCreate.Name);

            return projectRepository.Create(projectToCreate);
        }

        public Project Get(Guid id)
        {
            Project projcet = projectRepository.GetById(id);

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
            
            return projectRepository.Update(id, updatedProject);
        }

        public void Delete(Guid id)
        {
            projectRepository.Delete(id);
        }

        public void DeleteTester(Project oneProject, User tester)
        {
            Project project = projectRepository.GetById(oneProject.Id);

              if (project.Users.Contains(tester))
              {
                project.Users.Remove(tester);
              }
        }

        public void DeleteDeveloper(Project oneProject, User developer)
        {
            Project project = projectRepository.GetById(oneProject.Id);

            if (project.Users.Contains(developer))
            {
                project.Users.Remove(developer);
            }
        }

        public void AssignDeveloper(Project oneProject, User developer)
        {
            Project project = projectRepository.GetById(oneProject.Id);

            if (!project.Users.Contains(developer))
            {
                project.Users.Add(developer);
            }
        }

        public void AssignTester(Project oneProject, User tester)
        {
            Project project = projectRepository.GetById(oneProject.Id);

            if (!project.Users.Contains(tester))
            {
                project.Users.Add(tester);
            }
        }

        public List<Project> GetAll()
        {
            return projectRepository.GetAll();
        }

        public List<User> GetAllTesters(Project oneProject)
        {
            List<User> testers = new List<User>();

            Project project = projectRepository.GetById(oneProject.Id);

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

            Project project = projectRepository.GetById(oneProject.Id);

            foreach (User user in project.Users)
            {
                if (user.Rol.Name == Rol.developer)
                    developers.Add(user);
            }

            return developers;
        }

        public List<Bug> GetAllBugByProject(Project project)
        {
            return Get(project.Id).Bugs; 
        }

    }
}
