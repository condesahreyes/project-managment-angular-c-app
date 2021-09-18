using System;
using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IUserLogic
    {
        User Create(User userToCreate);
        User Get(Guid id);
        IEnumerable<User> GetAll();
    }
}
