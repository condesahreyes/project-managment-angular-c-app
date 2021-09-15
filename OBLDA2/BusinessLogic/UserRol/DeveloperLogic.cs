using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System.Linq;
using Domain;

namespace BusinessLogic.UserRol
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private IRepository<User> userRepository;
        private IRepository<Project> projectRepository;
        private IRepository<Rol> rolRepository;
        private IRepository<Bug> bugRepository;

        public DeveloperLogic(IRepository<User> userRepository, IRepository<Project> projectRepository, 
            IRepository<Rol> rolRepository, IRepository<Bug> bugRepository)
        {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
            this.rolRepository = rolRepository;
            this.bugRepository = bugRepository;
        }

        public User Create(User developerToCreate)
        {
            UserLogic userLogic = new UserLogic(userRepository, rolRepository);

            User developerCreate = userLogic.Create(developerToCreate);

            return developerCreate;
        }

        public User GetByString(string userName)
        {
            return userRepository.GetAll().Where(user => (user.UserName == userName)).First();
        }

        public List<Bug> GetAllBugs(User developer)
        {
            IEnumerable<Project> allProjects = projectRepository.GetAll();

            List<Bug> bugs = new List<Bug>();

            foreach (var project in allProjects)
                if (project.desarrolladores.Contains(developer))
                    bugs.AddRange(project.incidentes);

            return bugs;
        }

        public Bug UpdateStateToActiveBug(Bug bug)
        {
            Bug activeBug = CloneBug(bug);

            activeBug.State = "Activo";

            bugRepository.Update(bug, activeBug);

            return activeBug;
        }

        public Bug UpdateStateToDoneBug(Bug bug)
        {
            Bug doneBug = CloneBug(bug);

            doneBug.State = "Resuelto";

            bugRepository.Update(bug, doneBug);

            return doneBug;
        }

        private Bug CloneBug(Bug bug)
        {
            return new Bug(bug.Project, bug.Id, bug.Name, bug.Domain, bug.Version, bug.State);
        }
    }
}