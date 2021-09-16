using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using System;
using Domain;
<<<<<<< HEAD
=======
using DataAccessInterface;
using System.Linq;
>>>>>>> administratorLogicTest

namespace BusinessLogic
{
    public class UserLogic
    {
<<<<<<< HEAD
        private IRepository<Rol> rolRepository;
        private IRepository<User> userDA;

        public UserLogic(IRepository<User> UserDA, IRepository<Rol> rolRepository)
        {
            this.rolRepository = rolRepository;
            this.userDA = UserDA;
        }

        public User Create(User userToCreate)
        {
            IsValidUser(userToCreate);
            NotExistUser(userToCreate);

            User userCreate = userDA.Create(userToCreate);

            return userCreate;
        }

        private void IsValidUser(User userToCreate)
        {
            ValidateRol(userToCreate.Rol);
            User.IsValidUser(userToCreate);
        }

        private void NotExistUser(User user)
        {
            bool existUser = userDA.GetAll().Any(u => (u.Email == user.Email));

            if (existUser)
            {
                throw new Exception("The user already exists");
            }
        }

        private void ValidateRol(Rol rol)
        {
            IEnumerable<Rol> roles = rolRepository.GetAll();

            if (!roles.Contains(rol))
                throw new Exception();
=======
        private IRepository<User,Guid> userDA;

        public UserLogic(IRepository<User,Guid> UserDA)
        {
            this.userDA = UserDA;
        }


        public User Create(User userToCreate)
        {
            if (!(ExistUser(userToCreate)) && User.CorrectData(userToCreate))
            {
                userDA.Create(userToCreate);
                userDA.Save();
                return userToCreate;
            }

            return null;
        }

        public bool ExistUser(User user)
        {
            var userToReturn = userDA.GetAll().Any(u => (u.Email == user.Email));
            if (userToReturn == null)
            {
                throw new Exception("The user already exists");

            }

            return true;
        }

        public User Get(Guid id)
        {

            User user = userDA.Get(id);

            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            return user;
>>>>>>> administratorLogicTest
        }

    }
}
