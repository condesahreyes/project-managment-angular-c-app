using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic.UserRol
{
    public class TesterLogic : ITesterLogic
    {

        private IRepository<User, Guid> userRepository;
        private IRepository<Project, Guid> projectRepository;
        private IRepository<Rol, Guid> rolRepository;
        private IRepository<Bug, int> bugRepository;

        public TesterLogic(IRepository<User, Guid> userRepository, IRepository<Project, Guid> projectRepository,
            IRepository<Rol, Guid> rolRepository, IRepository<Bug, int> bugRepository)
        {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
            this.rolRepository = rolRepository;
            this.bugRepository = bugRepository;
        }


        public IEnumerable<User> GetAll()
        {
            List<User> users = (List<User>)userRepository.GetAll();

            List<User> testers = new List<User>();
            foreach (User user in users)
            {
                if (user.Rol.Name == "Tester")
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

        public IEnumerable<Project> GetProjectsByTester(Guid id)
        {
            throw new NotImplementedException();
        }

        public Bug CreateBug(Bug bug)
        {
            return bugRepository.Create(bug);
        }

        public List<Bug> GetAllBugs(User tester)
        {
            IEnumerable<Project> allProjects = projectRepository.GetAll();

            List<Bug> bugs = new List<Bug>();

            foreach (var project in allProjects)
                if (project.desarrolladores.Contains(tester))
                    bugs.AddRange(project.incidentes);

            return bugs;
        }

        public void DeleteBug(int id)
        {
            bugRepository.Delete(id);
        }
    }
}
