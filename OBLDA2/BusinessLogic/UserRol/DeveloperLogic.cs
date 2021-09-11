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
        private IDeveloperLogic developerRepository;
        private IRepository<User> userRepository;

        public DeveloperLogic(IDeveloperLogic developerRepository, IRepository<User> userRepository)
        {
            this.developerRepository = developerRepository;
            this.userRepository = userRepository;
        }

        public User Create(User developer)
        {
            User validUser = CreateValidDeveloper(developer);

            if (ExistDeveloper(developer))
            {
                throw new Exception();//Hacer refactor, crear exception
            }

            userRepository.Create(validUser);
            userRepository.Save(); // Ver si aplica
            return validUser;
        }

        private User CreateValidDeveloper(User developer)
        {
            Guid validId = developer.Id;

            string validName = UserLogic.ValidateName(developer.Name);
            string validSurename = UserLogic.ValidateSurname(developer.LastName);
            string validUserName = UserLogic.ValidateUserName(developer.UserName);
            string validEmail = UserLogic.ValidateEmail(developer.Email);
            string validPassword = developer.Password;

            Rol validRol = developer.Rol;

            return new User(validId, validName, validSurename, validUserName,
                validPassword, validEmail, validRol);
        }

        private bool ExistDeveloper(User developer)
        {
            return userRepository.GetAll().Any(user => (user.UserName == developer.UserName));
        }

        public User Get(string userName)
        {
            return (User)userRepository.GetAll().Where(user => (user.UserName == userName));
        }

        public List<Bug> GetAllBugs()
        {
            IRepository<Project> projectRepository;
            IEnumerable<User> 
            throw new NotImplementedException();
        }
    }
}