using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface IUserLogic
    {
        User Create(User userToCreate);
        User Get(Guid id);

        List<User> GetAll();
        List<string> GetAllTokens();

        void Update(User user);
        void ExistUser(User user);
    }
}
