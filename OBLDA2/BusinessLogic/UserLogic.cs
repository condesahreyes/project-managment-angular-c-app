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
        private IUserRepository userDA;
        private IRepository<Rol, Guid> rolRepository;

        public UserLogic() { }

        public UserLogic(IUserRepository UserDA, IRepository<Rol, Guid> rolRepository)
        {
            this.userDA = UserDA;
            this.rolRepository = rolRepository;
        }

        public User Get(Guid id)
        {
            User user = userDA.GetById(id);

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
            IsValidUser(ref userToCreate);
            NotExistUser(userToCreate);
            userToCreate.Projects = new List<Project>();
            User userCreate = userDA.Create(userToCreate);

            return userCreate;
        }

        private void IsValidUser(ref User userToCreate)
        {
            userToCreate.Rol = ValidateRol(userToCreate.Rol);
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

        private Rol ValidateRol(Rol rol)
        {
            List<Rol> roles = rolRepository.GetAllGeneric();

            foreach (Rol oneRol in roles)
            {
                if (oneRol.Name.ToLower() == rol.Name.ToLower())
                {
                    return oneRol;
                }
            }

            throw new Exception();
        }

    }
}
