using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.UserRol;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class AdministratorLogic : IAdministratorLogic

    {
        private IRepository<User,Guid> admDA;
        private UserLogic userLogic;
        private ProjectLogic projectLogic;
        private BugLogic bugLogic;


        public AdministratorLogic(IRepository<User,Guid> AdmDA)
        {
            this.admDA = AdmDA;

        }

        public IEnumerable<User> GetAll()
        {
            return this.admDA.GetAll().Where(user => user.Rol.Name.Equals("Administrator")); 

        }

        public User Get(Guid id)
        {
            var getUser = this.userLogic.Get(id);

            if (!getUser.Rol.Name.Equals("Administrator"))
            {
                throw new Exception("Administrator does not exist");

            }
            return getUser;

        }

        public User Create(User admin1)
        {
            return userLogic.Create(admin1);

        }

        public Project CreteProject(Project projectToCreate)
        {
            return projectLogic.Create(projectToCreate);
        }

        public void UpdateProject(Guid id, Project updatedProject )
        {
            projectLogic.Update(id, updatedProject);
        }

        public void DeleteProject(Guid id)
        {
            projectLogic.Delete(id);
        }

        public void DeleteTesterByProject(Project project, Guid idTester)
        {
            projectLogic.DeleteTester(project, idTester);

        }
        public void DeleteDeveloperByProject(Project project, Guid idDeveloper)
        {
            projectLogic.DeleteDeveloper(project, idDeveloper);

        }

        public void CreteBug(Bug bugToCreate)
        {
            bugLogic.Create(bugToCreate);
        }

        public void UpdateBug(int id, Bug updatedBug)
        {
            bugLogic.Update(id, updatedBug);
        }

        public void DeleteBug(int id)
        {
            bugLogic.Delete(id);
        }

        public void AssignDeveloperByProject(Project project, Guid idDeveloper)
        {
            projectLogic.AssignDeveloper(project, idDeveloper);

        }
        
        public void AssignTesterByProject(Project project, Guid idTester)
        {
            projectLogic.AssignTester(project, idTester);

        }

        public void ImportBugsByProjectByProvider(Project project, List<Bug> bugsProject)
        {
            projectLogic.ImportBugsByProvider(project, bugsProject);
        }

        public List<Project> GetTotalBugByAllProject()
        {
            return projectLogic.GetAll();
           
        }

        public int GetFixedBugsByDeveloper()
        {
            return projectLogic.GetAllFixedBugsByDeveloper();
        }

    }
}
