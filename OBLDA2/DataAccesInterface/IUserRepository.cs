using System.Collections.Generic;
using Domain;
using System;

namespace DataAccessInterface
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        List<User> GetAll();
        User GetById(Guid id);
        void UpdateUser(User user);
    }
}
