using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using System.Linq;
using Exceptions;
using System;
using Domain;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private const string invalidRol = "You must entry a valid rol";
        private const string notExistUser = "User not exist";
        private const string existingUser = "The user already exists";


        private IUserRepository userDA;
        private IRepository<Rol, Guid> rolRepository;

        public UserLogic() { }

        public UserLogic(IUserRepository UserDA, IRepository<Rol, Guid> rolRepository)
        {
            this.userDA = UserDA;
            this.rolRepository = rolRepository;
        }

        public UserLogic(IUserRepository UserDA)
        {
            this.userDA = UserDA;
        }

        public User Get(Guid id)
        {
            User user = userDA.GetById(id);

            if (user == null)
            {
                throw new NoObjectException(notExistUser);
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

        public void ExistUser(User user)
        {
            Get(user.Id);
        }

        public void Update(User user) 
        {
            IsValidUser(ref user);
            userDA.UpdateUser(user);
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
                throw new ExistingObjectException(existingUser);
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

            throw new InvalidDataObjException(invalidRol);
        }

        public List<string> GetAllTokens()
        {
            List<string> tokens = new List<string>();
            List<User> users = GetAll();

            foreach (var user in users)
            {
                if (user.Token != null)
                {
                    tokens.Add(user.Token);
                }
            }

            return tokens;
        }

    }
}
