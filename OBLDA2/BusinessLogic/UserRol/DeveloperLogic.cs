﻿using System.Collections.Generic;
using BusinessLogicInterface;
using Exceptions;
using Domain;
using System;

namespace BusinessLogic.UserRol
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private const string unassociatedBugDeveloper = "This bug is unassociated to developer";
        private const string notUserDeveloper = "This user rol is not Developer";
        private const string invalidState = "It´s not a valid state bug";

        private IProjectLogic projectLogic;
        private IUserLogic userLogic;
        private IBugLogic bugLogic;

        public DeveloperLogic(IUserLogic userLogic, IProjectLogic projectLogic, IBugLogic bugLogic)
        {
            this.userLogic = userLogic;
            this.projectLogic = projectLogic;
            this.bugLogic = bugLogic;
        }

        public List<User> GetAll()
        {
            List<User> users = userLogic.GetAll();
            List<User> developers = new List<User>();

            foreach (User user in users)
            {
                if (user.Rol.Name.ToLower() == Rol.developer.ToLower())
                {
                    developers.Add(user);
                }
            }

            return developers;
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
                if (project.Users.Contains(developer))
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

        public List<Project> GetAllProjects(Guid developerId)
        {
            List<Project> allProjects = projectLogic.GetAll();
            List<Project> projectToReturn = new List<Project>();

            foreach (var project in allProjects)
            {
                if (project.Users.Find(u => u.Id == developerId) != null)
                {
                    projectToReturn.Add(project);
                }
            }

            return projectToReturn;
        }

        public List<Task> GetAllTask(Guid developerId)
        {
            GetDeveloper(developerId);
            List<Project> projects = GetAllProjects(developerId);
            List<Task> tasksToReturn = new List<Task>();

            foreach (var project in projects)
            {
                if (project.Users.Find(u => u.Id == developerId) != null)
                {
                    tasksToReturn.AddRange(project.Tasks);
                }
            }

            return tasksToReturn;
        }

        private void ItsBugDeveloper(int id, Guid userResolved)
        {
            List<Bug> bugs = GetAllBugs(userResolved);

            if (bugs.Find(b => b.Id == id) == null)
            {
                throw new InvalidDataObjException(unassociatedBugDeveloper);
            }
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