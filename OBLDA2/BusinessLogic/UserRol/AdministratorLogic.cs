using System.Collections.Generic;
using BusinessLogicInterface;
using System.Linq;
using System;
using Domain;

namespace BusinessLogic
{
    public class AdministratorLogic : IAdministratorLogic
    {
        private IUserLogic userLogic;
        private IProjectLogic projectLogic;
        private IBugLogic bugLogic;
        private ITesterLogic testerLogic;

        public AdministratorLogic(IUserLogic userLogic, 
            IProjectLogic projectLogic, ITesterLogic testerLogic, IBugLogic bugLogic)
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

        public List<User> GetAll()
        {
            return (List<User>)this.userLogic.GetAll().Where(user => user.Rol.Name.Equals("Administrator"));
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

        public void DeleteTesterByProject(Project project, User tester)
        {
            projectLogic.DeleteTester(project, tester);
        }

        public void DeleteDeveloperByProject(Project project, User developer)
        {
            projectLogic.DeleteDeveloper(project, developer);
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

        public void AssignDeveloperByProject(Project project, User developer)
        {
            projectLogic.AssignDeveloper(project, developer);
        }

        public void AssignTesterByProject(Project project, User tester)
        {
            projectLogic.AssignTester(project, tester);
        }

        public void ImportBugsByProjectByProvider(Project project, List<Bug> bugsProject)
        {
            //projectLogic.ImportBugsByProvider(project, bugsProject);
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
            List<Bug> bugs = (List<Bug>)bugLogic.GetAll();
            List<Bug> bugsByDeveloper = new List<Bug>();

            foreach (Bug bug in bugs)
            {
                if(bug.SolvedBy != null && bug.SolvedBy.Id == id)
                {
                    bugsByDeveloper.Add(bug);
                }
            }

            return bugsByDeveloper.Count;
        }

    }
}