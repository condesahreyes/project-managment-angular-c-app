using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;
using Exceptions;

namespace BusinessLogic.UserRol
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private const string invalidState = "It´s not a valid state bug";

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

        public Bug UpdateState(int id, string state, Guid userResolved)
        {
            if (state.ToLower() == State.active.ToLower())
                return UpdateStateToActiveBug(id, userResolved);
            else if (state.ToLower() == State.done.ToLower())
                return UpdateStateToDoneBug(id, userResolved);

            throw new InvalidDataObjException(invalidState);
        }

        public Bug UpdateStateToActiveBug(int id, Guid userId)
        {
            Bug activeBug = bugLogic.Get(id);

            activeBug.State.Name = State.active;
            activeBug.SolvedBy = null;

            bugLogic.Update(id, activeBug);

            return activeBug;
        }

        public Bug UpdateStateToDoneBug(int id, Guid userId)
        {
            Bug doneBug = bugLogic.Get(id);

            doneBug.State.Name = State.done;
            doneBug.SolvedBy = userLogic.Get(userId);

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