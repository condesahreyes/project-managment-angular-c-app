using System.Text.RegularExpressions;
using System;
using Domain;
using DataAccessInterface;
using System.Linq;

namespace BusinessLogic
{
    public class UserLogic
    {
        private IRepository<User,Guid> userDA;

        public UserLogic(IRepository<User,Guid> UserDA)
        {
            this.userDA = UserDA;
        }


        public User Create(User userToCreate)
        {
            if (!(ExistUser(userToCreate)) && User.CorrectData(userToCreate))
            {
                Rol.IsValidRolName(userToCreate.Rol.Name);
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
        }

    }
}
