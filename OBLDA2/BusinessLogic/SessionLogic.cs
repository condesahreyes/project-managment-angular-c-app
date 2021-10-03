using System.Collections.Generic;
using BusinessLogicInterface;
using System;
using Domain;
using System.Security.Authentication;
using System.Linq;

namespace BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private IUserLogic userLogic;
        private readonly string InvalidEmailOrPasswordMessage = "Invalid email or password.";

        public SessionLogic(IUserLogic _userLogic)
        {
            userLogic = _userLogic;
        }
        public SessionLogic(){}

        public List<string> GetAllTokens()
        {
            List<string> tokens = new List<string>();
            var users = userLogic.GetAll();
            foreach (var user in users)
            {
                if (user.Token != null) {
                    tokens.Add(user.Token);
                }
            }
            return tokens;

        }
        public bool IsCorrectToken(string token)
        {
            return GetAllTokens().Contains(token);
        }

        public string Login(string email, string password)
        {
            User admin = GetUsers(email, password);

            if (admin == null)
            {
                throw new InvalidCredentialException(InvalidEmailOrPasswordMessage);
            }

            return GenerateAndInsertToken(admin);
        }

        public User GetUsers(string email, string password)
        {
            List<User> admins = userLogic.GetAll();
            return admins.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        public string GenerateAndInsertToken(User user)
        {
            string token = "";

            if (user.Rol.Name.ToLower().Equals(Rol.administrator.ToLower()))
            {
                token = "a" + Guid.NewGuid().ToString();
            }

            else if (user.Rol.Name.ToLower().Equals(Rol.tester.ToLower()))
            {
                token = "t" + Guid.NewGuid().ToString();
            }

            else if (user.Rol.Name.ToLower().Equals(Rol.developer.ToLower()))
            {
                token = "d" + Guid.NewGuid().ToString();
            }

            UpdateToken(user, token);
           
            return token;
        }

        private void UpdateToken(User user, string token)
        {
            User usu = userLogic.Get(user.Id);
            usu.Token = token;
            userLogic.Update(user);
        }
        public bool Logout(string token)
        {
            var userToLogOut = userLogic.GetAll().Where(u => u.Token == token);
            UpdateToken(userToLogOut.First(), null);
            return true;
        }

    }
}
