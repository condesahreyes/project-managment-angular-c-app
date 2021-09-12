using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic.UserRol
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private IRepository<User> userRepository;
        private IRepository<Project> projectRepository;

        public DeveloperLogic(IRepository<User> userRepository, IRepository<Project> projectRepository)
        {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;
        }

        public User Create(User developer)
        {
            IsValidDeveloper(developer);

            if (ExistDeveloper(developer))
            {
                throw new Exception();//Hacer refactor, crear exception
            }

            userRepository.Create(developer);
            //userRepository.Save(); // Ver si aplica
            return developer;
        }

        private void IsValidDeveloper(User developer)
        {
            string validName = UserLogic.ValidateName(developer.Name);
            string validSurename = UserLogic.ValidateSurname(developer.LastName);
            string validUserName = UserLogic.ValidateUserName(developer.UserName);
            string validEmail = UserLogic.ValidateEmail(developer.Email);
        }

        private bool ExistDeveloper(User developer)
        {
            return userRepository.GetAll().Any(user => (user.UserName == developer.UserName));
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
    }
}