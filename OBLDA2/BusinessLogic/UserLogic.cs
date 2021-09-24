using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System.Linq;
using System;
using Domain;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private IRepository<User, Guid> userDA;
        private IRepository<Rol, Guid> rolRepository;

        public UserLogic() { }

        public UserLogic(IRepository<User, Guid> UserDA, IRepository<Rol, Guid> rolRepository)
        {
            this.userDA = UserDA;
            this.rolRepository = rolRepository;
        }

        public User Get(Guid id)
        {
            User user = userDA.Get(id);

            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            return user;
        }

        public List<User> GetAll()
        {
            return userDA.GetAll();
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
            List<Rol> roles = rolRepository.GetAll();

            if (!roles.Contains(rol))
                throw new Exception();
        }

    }
}
