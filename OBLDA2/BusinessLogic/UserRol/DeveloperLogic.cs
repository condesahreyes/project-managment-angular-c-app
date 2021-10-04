using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;

namespace BusinessLogic.UserRol
{
    public class DeveloperLogic : IDeveloperLogic
    {

        private IUserLogic userLogic;
        private IProjectLogic projectLogic;
        private IBugLogic bugLogic;

        public DeveloperLogic(IUserLogic userLogic, IProjectLogic projectLogic,
            IRepository<Rol, Guid> rolRepository, IBugLogic bugLogic)
        {
            this.userLogic = userLogic;
            this.projectLogic = projectLogic;
            this.bugLogic = bugLogic;
        }

        public DeveloperLogic() { }

        public User Create(User developerToCreate)
        {
            User developerCreate = userLogic.Create(developerToCreate);

            return developerCreate;
        }

        public User GetByString(string userName)
        {
            return userLogic.GetAll().Where(user => (user.UserName == userName)).First();
        }

        public Bug UpdateStateToActiveBug(int id)
        {
            Bug activeBug = bugLogic.Get(id);

            activeBug.State.Name = State.active;

            bugLogic.Update(id, activeBug);

            return activeBug;
        }

        public Bug UpdateStateToDoneBug(int id)
        {
            Bug doneBug = bugLogic.Get(id);

            doneBug.State.Name = State.done;

            bugLogic.Update(id, doneBug);

            return doneBug;
        }

        public void AssignDeveloperToProject(Project project, User developer)
        {
            projectLogic.AssignDeveloper(project, developer);
        }

        public void DeleteDeveloperInProject(Project project, User developer)
        {
            projectLogic.DeleteDeveloper(project, developer);
        }

        public int CountBugDoneByDeveloper(User developer)
        {
            int countBugsResolved = 0;

            List<Bug> bugs = GetAllBugs(developer);

            foreach (Bug bug in bugs)
            {
                if(bug.SolvedBy.Id == developer.Id)
                {
                    countBugsResolved++;
                }
            }

            return countBugsResolved;
        }

        public List<Bug> GetAllBugs(User developer)
        {
            List<Project> allProjects = projectLogic.GetAll();

            List<Bug> bugs = new List<Bug>();

            foreach (var project in allProjects)
                if (project.Users.Contains(developer))
                    bugs.AddRange(project.Bugs);

            return bugs;
        }
    }
}