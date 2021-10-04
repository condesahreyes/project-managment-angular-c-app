using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Exceptions;
using System;
using Domain;

namespace BusinessLogic
{
    public class ProjectLogic : IProjectLogic
    {
        private const string notExistProject = "Project does not exist";

        private IProjectRepository projectRepository;
        private IUserLogic userLogic;

        public ProjectLogic() { }

        public ProjectLogic(IProjectRepository ProjectDa, IUserLogic userLogic)
        {
            this.projectRepository = ProjectDa;
            this.userLogic = userLogic;
        }

        public Project Create(Project projectToCreate)
        {
            Project.ValidateName(projectToCreate.Name);
            projectToCreate.Bugs = new List<Bug>();
            projectToCreate.Users = new List<User>();
            return projectRepository.Create(projectToCreate);
        }

        public Project Get(Guid id)
        {
            Project projcet = projectRepository.GetById(id);

            if (projcet == null)
            {
                throw new NoObjectException(notExistProject);
            }

            return projcet;
        }

        public void ExistProject(Guid projectId)
        {
            Get(projectId);
        }

        public Project Update(Guid id, Project updatedProject)
        {
            ExistProject(id);
            Project.ValidateName(updatedProject.Name);
            return projectRepository.Update(id, updatedProject);
        }

        public void Delete(Guid id)
        {
            ExistProject(id);
            projectRepository.Delete(id);
        }

        public void DeleteTester(Project oneProject, User tester)
        {
            Project project = Get(oneProject.Id);
            userLogic.ExistUser(tester);

            if (project.Users.Contains(tester))
            {
                project.Users.Remove(tester);
            }
        }

        public void DeleteDeveloper(Project oneProject, User developer)
        {
            Project project = Get(oneProject.Id);
            userLogic.ExistUser(developer);

            if (project.Users.Contains(developer))
            {
                project.Users.Remove(developer);
            }
        }

        public void AssignDeveloper(Project oneProject, User developer)
        {
            Project project = Get(oneProject.Id);
            userLogic.ExistUser(developer);

            if (!project.Users.Contains(developer))
            {
                project.Users.Add(developer);
            }
        }

        public void AssignTester(Project oneProject, User tester)
        {
            Project project = Get(oneProject.Id);
            userLogic.ExistUser(tester);

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
            return GetAllUserByRol(Rol.tester, oneProject);
        }

        public List<User> GetAllDevelopers(Project oneProject)
        {
            return GetAllUserByRol(Rol.developer, oneProject);
        }

        private List<User> GetAllUserByRol(string rol, Project oneProject)
        {
            List<User> users = new List<User>();

            Project project = Get(oneProject.Id);

            foreach (User user in project.Users)
            {
                if (user.Rol.Name.ToLower() == rol.ToLower())
                    users.Add(user);
            }

            return users;
        }

        public List<Bug> GetAllBugByProject(Project project)
        {
            return Get(project.Id).Bugs; 
        }

    }
}
