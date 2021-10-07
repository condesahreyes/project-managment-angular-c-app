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
        private const string notExistUser = "User not exist";
        private const string existingUser = "The user already exists";
        private const string invalidRol = "You must entry a valid rol";
        
        private IUserRepository userRepository;
        private IRepository<Rol, Guid> rolRepository;

        public UserLogic(IUserRepository userRepository, IRepository<Rol, Guid> rolRepository)
        {
            this.userRepository = userRepository;
            this.rolRepository = rolRepository;
        }

        public User Get(Guid id)
        {
            User user = userRepository.GetById(id);

            if (user == null)
            {
                throw new NoObjectException(notExistUser);
            }

            return user;
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User Create(User userToCreate)
        {
            IsValidUser(ref userToCreate);
            NotExistUser(userToCreate);

            userToCreate.Projects = new List<Project>();

            User userCreate = userRepository.Create(userToCreate);

            return userCreate;
        }

        public void Update(User user) 
        {
            IsValidUser(ref user);
            userRepository.UpdateUser(user);
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

        private void IsValidUser(ref User userToCreate)
        {
            userToCreate.Rol = ValidateRol(userToCreate.Rol);
            User.IsValidUser(userToCreate);
        }

        private void NotExistUser(User user)
        {
            bool existUser = userRepository.GetAll().Any(u => (u.Email == user.Email));

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

    }
}
