using Domain;
using System;
using System.Collections.Generic;

namespace DataAccessInterface
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        List<User> GetAll();
        User GetById(Guid id);
        void UpdateUser(User user);
    }
}
