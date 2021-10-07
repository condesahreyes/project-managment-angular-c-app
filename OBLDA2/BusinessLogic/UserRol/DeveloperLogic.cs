using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Exceptions;
using Domain;
using System;

namespace BusinessLogic.UserRol
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private const string invalidState = "It´s not a valid state bug";
        private const string notUserDeveloper = "This user rol is not Developer";
        private const string unassociatedBugDeveloper = "This bug is unassociated to developer";

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

        public void AssignDeveloperToProject(Guid projectId, Guid developerId)
        {
            User developer = GetDeveloper(developerId);

            projectLogic.AssignUser(projectId, ref developer);
        }

        public void DeleteDeveloperInProject(Guid projectId, Guid developerId)
        {
            User developer = GetDeveloper(developerId);

            projectLogic.DeleteUser(projectId, ref developer);
        }

        public int CountBugDoneByDeveloper(Guid developerId)
        {
            User developer = GetDeveloper(developerId);

            int countBugsResolved = 0;

            List<Project> projects = projectLogic.GetAll();
            foreach (Project project in projects)
            {
                foreach (Bug bug in project.Bugs)
                {
                    if (bug.SolvedBy != null && bug.SolvedBy.Id == developer.Id)
                    {
                        countBugsResolved++;
                    }
                }
            }

            return countBugsResolved;
        }

        public List<Bug> GetAllBugs(Guid developerId)
        {
            User developer = GetDeveloper(developerId);

            List<Project> allProjects = projectLogic.GetAll();

            List<Bug> bugs = new List<Bug>();

            foreach (var project in allProjects)
            {
                if (project.Users.Contains(developer))
                {
                    bugs.AddRange(project.Bugs);
                }
            }

            return bugs;
        }

        public Bug UpdateState(int id, string state, Guid userResolved)
        {
            ItsBugDeveloper(id, userResolved);

            Bug bug = bugLogic.Get(id, userResolved);

            if (state.ToLower() == State.active.ToLower())
            {
                UpdateStateToActiveBug(ref bug);
            }
            else if (state.ToLower() == State.done.ToLower())
            {
                UpdateStateToDoneBug(ref bug, userResolved);
            }
            else
            {
                throw new InvalidDataObjException(invalidState);
            }

            bugLogic.Update(id, bug, userResolved);

            return bug;
        }

        private void ItsBugDeveloper(int id, Guid userResolved)
        {
            List<Bug> bugs = GetAllBugs(userResolved);

            if (bugs.Find(b => b.Id == id) == null)
                throw new InvalidDataObjException(unassociatedBugDeveloper);
        }

        private User GetDeveloper(Guid developerId)
        {
            User developer = userLogic.Get(developerId);

            IsDeveloper(developer);

            return developer;
        }

        private void IsDeveloper(User developer)
        {
            if (developer.Rol.Name.ToLower() != Rol.developer.ToLower())
            {
                throw new InvalidDataObjException(notUserDeveloper);
            }
        }

        private void UpdateStateToActiveBug(ref Bug bug)
        {
            bug.State = new State(State.active);
            bug.SolvedBy = null;
        }

        private void UpdateStateToDoneBug(ref Bug bug, Guid resolvedById)
        {
            bug.State = new State(State.done);
            bug.SolvedBy = userLogic.Get(resolvedById);
        }
    }
}