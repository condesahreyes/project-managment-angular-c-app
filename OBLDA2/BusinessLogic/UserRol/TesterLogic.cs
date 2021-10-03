using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using System;

namespace BusinessLogic.UserRol
{
    public class TesterLogic : ITesterLogic
    {
        private IUserRepository userRepository;
        private IRepository<Project, Guid> projectRepository;
        private IRepository<Rol, Guid> rolRepository;
        private IRepository<Bug, int> bugRepository;
        private IProjectLogic projcetLogic;

        public TesterLogic(IUserRepository userRepository, IRepository<Project, Guid> projectRepository,
            IRepository<Rol, Guid> rolRepository, IRepository<Bug, int> bugRepository)
        {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
            this.rolRepository = rolRepository;
            this.bugRepository = bugRepository;
        }

        public TesterLogic() { }

        public List<User> GetAll()
        {
            List<User> users = userRepository.GetAll();

            List<User> testers = new List<User>();
            foreach (User user in users)
            {
                if (user.Rol.Name == Rol.tester)
                    testers.Add(user);
            }

            return testers;
        }

        public User Get(Guid id)
        {
            return userRepository.Get(id);
        }

        public User Create(User tester)
        {
            UserLogic userLogic = new UserLogic(userRepository, rolRepository);

            User developerCreate = userLogic.Create(tester);

            return developerCreate;
        }

        public void AssignTesterToProject(Project project, User tester)
        {
            projcetLogic.AssignTester(project, tester);
        }

        public void AssignDeveloperToProject(Project project, User tester)
        {
            projcetLogic.AssignDeveloper(project, tester);
        }

        public void DeleteTesterInProject(Project project, User tester)
        {
            projcetLogic.DeleteTester(project, tester);
        }

        public List<Project> GetProjectsByTester(Guid id)
        {
            throw new NotImplementedException();
        }

        public Bug CreateBug(Bug bug)
        {
            return bugRepository.Create(bug);
        }

        public List<Bug> GetAllBugs(User tester)
        {
            List<Project> allProjects = projectRepository.GetAllGeneric();

            List<Bug> bugs = new List<Bug>();

            foreach (var project in allProjects)
                if (project.Users.Contains(tester))
                    bugs.AddRange(project.Bugs);

            return bugs;
        }

        public void DeleteBug(int id)
        {
            bugRepository.Delete(id);
        }

    }
}
