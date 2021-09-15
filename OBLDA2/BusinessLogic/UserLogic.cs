using System.Text.RegularExpressions;
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

        private void NotExistUser(User user)
        {
            bool existUser = userDA.GetAll().Any(u => (u.Email == user.Email));

            if (existUser)
            {
                throw new Exception("The user already exists");
            }
        }

        private void IsValidUser(User user)
        {
            ValidateName(user.Name);
            ValidateLastName(user.LastName);
            ValidateUserName(user.UserName);
            ValidateRol(user.Rol);
            ValidatePassword(user.Password);
            ValidateEmail(user.Email);
        }

        private void ValidateName(string name)
        {
            if (name.Length < 1)
                throw new Exception();
        }

        private void ValidateLastName(string lastName)
        {
            if (lastName.Length < 1)
                throw new Exception();
        }

        private void ValidateUserName(string userName)
        {
            if (userName.Length < 1)
                throw new Exception();
        }

        private void ValidateRol(Rol rol)
        {
            IEnumerable<Rol> roles = rolRepository.GetAll();

            if (!roles.Contains(rol))
                throw new Exception();
        }

        private void ValidatePassword(string password)
        {
            if (password.Length < 1)
                throw new Exception();
        }

        private void ValidateEmail(string email)
        {
            string character = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (!Regex.IsMatch(email, character))
                throw new Exception();
        }
    }
}
