using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Exceptions;
using Domain;
using System;

namespace BusinessLogic.UserRol
{
    public class TesterLogic : ITesterLogic
    {
        private const string notUserTester = "This user rol is not Tester";

        private IUserLogic userLogic;
        private IProjectLogic projcetLogic;

        public TesterLogic(IUserLogic userLogic, 
            IProjectLogic projcetLogic, IRepository<Rol, Guid> rolRepository)
        {
            this.userLogic = userLogic;
            this.projcetLogic = projcetLogic;
        }

        public User Get(Guid id)
        {
            return userLogic.Get(id);
        }

        public void AssignTesterToProject(Guid projectId, Guid testerId)
        {
            User tester = userLogic.Get(testerId);
            IsTester(tester);
            Project project = projcetLogic.Get(projectId);
            projcetLogic.AssignUser(projectId, ref tester);
        }

        public List<User> GetAll()
        {
            List<User> users = userLogic.GetAll();
            List<User> testers = new List<User>();

            foreach (User user in users)
            {
                if (user.Rol.Name == Rol.tester)
                    testers.Add(user);
            }

            return testers;
        }

        public void DeleteTesterInProject(Guid projectId, Guid testerId)
        {
            User tester = userLogic.Get(testerId);
            IsTester(tester);
            Project project = projcetLogic.Get(projectId);
            projcetLogic.DeleteUser(projectId, ref tester);
        }

        public List<Bug> GetAllBugs(Guid testerId)
        {
            User tester = Get(testerId);
            List<Project> allProjects = projcetLogic.GetAll();
            List<Bug> bugs = new List<Bug>();

            foreach (var project in allProjects)
                if (project.Users.Find(u => u.Id == testerId) != null)
                    bugs.AddRange(project.Bugs);

            return bugs;
        }
        public List<Bug> GetAllBugsByTesterInProject(Guid testerId, Guid project)
        {

            List<Bug> bugsByTester = this.GetAllBugs(testerId);
            List<Bug> allBugsInProject = this.projcetLogic.GetAllBugByProject(project);
            List<Bug> bugsInProjectWithTester = new List<Bug>();

            foreach (var bugTester in bugsByTester){
                foreach (var bugInProject in allBugsInProject){
                    if (bugTester.Equals(bugInProject))
                    {
                        bugsInProjectWithTester.Add(bugTester);
                    }
                }
            }
               
            return bugsInProjectWithTester;
        }


        public List<Project> GetAllProjects(Guid testerId)
        {
            User tester = Get(testerId);
            List<Project> allProjects = projcetLogic.GetAll();
            List<Project> projectToReturn = new List<Project>();

            foreach (var project in allProjects)
                if (project.Users.Find(u => u.Id == testerId) != null)
                    projectToReturn.Add(project);

            return projectToReturn;
        }

        private void IsTester(User tester)
        {
            if (tester.Rol.Name.ToLower() != Rol.tester.ToLower())
            {
                throw new InvalidDataObjException(notUserTester);
            }
        }
    }
}
