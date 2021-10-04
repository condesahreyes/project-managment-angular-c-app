using System.Security.Authentication;
using System.Collections.Generic;
using BusinessLogicInterface;
using System.Linq;
using System;
using Domain;

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
            return userLogic.GetAllTokens();
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
            User userDataAcces = userLogic.Get(user.Id); 

            string token = GenerateToken(userDataAcces);

            UpdateToken(user, token);
           
            return token;
        }

        private string GenerateToken(User user)
        {
            return user.Rol.Name + Guid.NewGuid().ToString();
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
