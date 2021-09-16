using System.Collections.Generic;
using DataAccessInterface;
using System.Linq;
using System;
using Domain;

namespace BusinessLogic
{
    public class UserLogic
    {
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
        }
    }
}
