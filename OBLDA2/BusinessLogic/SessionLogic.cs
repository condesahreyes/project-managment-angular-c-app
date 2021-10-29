using System.Collections.Generic;
using BusinessLogicInterface;
using System.Linq;
using Exceptions;
using System;
using Domain;

namespace BusinessLogic
{
    public class SessionLogic : ISessionLogic
    {
        private const string invalidEmailOrPasswordMessage = "Invalid email or password.";
        private const string invalidTokenMessage = "Invalid token.";

        private IUserLogic userLogic;

        public SessionLogic(IUserLogic _userLogic)
        {
            userLogic = _userLogic;
        }

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
                throw new NoObjectException(invalidEmailOrPasswordMessage);
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
            return user.Rol.Name + "-" + Guid.NewGuid().ToString();
        }

        private void UpdateToken(User user, string token)
        {
            User oneUser = userLogic.Get(user.Id);
            oneUser.Token = token;
            userLogic.Update(oneUser);
        }

        public void Logout(string token)
        {
            var userToLogOut = userLogic.GetAll().Where(u => u.Token == token);
            if (userToLogOut.Count() == 0)
                throw new InvalidDataObjException(invalidTokenMessage);
            UpdateToken(userToLogOut.First(), null);
        }

        public string GetUserIdWithToekn(string token)
        {
            var userId = "";
            if (IsCorrectToken(token))
            {
                List<User> users = userLogic.GetAll();
                foreach (var user in users)
                {
                    if (user.Token != null && user.Token.Equals(token))
                    {
                        userId = user.Id.ToString();
                    }
                }
            }
            return userId;
        }
    }
}

