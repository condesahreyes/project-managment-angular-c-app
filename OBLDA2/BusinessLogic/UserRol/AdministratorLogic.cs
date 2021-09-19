using BusinessLogicInterface;
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
        private IUserLogic userLogic;
        private IProjectLogic projectLogic;
        private IBugLogic bugLogic;
        private ITesterLogic testerLogic;



        public AdministratorLogic(IUserLogic userLogic, IProjectLogic projectLogic, ITesterLogic testerLogic, IBugLogic bugLogic)
        {
            this.userLogic = userLogic;
            this.projectLogic = projectLogic;
            this.bugLogic = bugLogic;
            this.testerLogic = testerLogic;

        }

        public User Create(User adminToCreate)
        {
            return userLogic.Create(adminToCreate);

        }


        public IEnumerable<User> GetAll()
        {
            return this.userLogic.GetAll().Where(user => user.Rol.Name.Equals("Administrator"));

        }

        public User Get(Guid id)
        {
            var getUser = this.userLogic.Get(id);

            if (getUser == null || !getUser.Rol.Name.Equals("Administrator"))
            {
                throw new Exception("Administrator does not exist");

            }
            return getUser;

        }

        public Project CreteProject(Project projectToCreate)
        {
            return projectLogic.Create(projectToCreate);
        }

        public List<Project> GetAllProject()
        {
            return projectLogic.GetAll();

        }

        public Project UpdateProject(Guid id, Project updatedProject)
        {
            return projectLogic.Update(id, updatedProject);
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

        public Bug CreateBug(Bug bugToCreate)
        {
            return bugLogic.Create(bugToCreate);
        }

        public Bug UpdateBug(int id, Bug updatedBug)
        {
            return bugLogic.Update(id, updatedBug);
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

        public int GetTotalBugByAllProject()
        {
            int total = 0;
            var projects = projectLogic.GetAll();
            foreach (var project in projects)
            {
                total += project.totalBugs;
            }
            return total;
        }

        public List<User> GetAllTesters(Project project)
        {
            return projectLogic.GetAllTesters(project);

        }

        public List<User> GetAllDevelopers(Project project)
        {
            return projectLogic.GetAllDevelopers(project);

        }

        public int GetFixedBugsByDeveloper(Guid id)
        {
            return projectLogic.GetAllFixedBugsByDeveloper(id);
        }

    }
}
