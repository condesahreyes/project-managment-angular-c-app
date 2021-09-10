using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class AdministratorLogic : IAdministratorLogic

    {
        private IRepository<User> admDA;

        public AdministratorLogic(IRepository<User> AdmDA)
        {
            this.admDA = AdmDA;

        }



        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public User Create(User admin1)
        {
            throw new NotImplementedException();
        }
    }
}
