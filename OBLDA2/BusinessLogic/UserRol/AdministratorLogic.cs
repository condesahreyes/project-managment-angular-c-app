using System;
using System.Collections.Generic;
using System.Linq;
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
            return this.admDA.GetAll().Where(user => user.Rol.Equals("Administrator")); //no creo que este bien, porque ROL es una calse que tiene un nombre.

        }

        public User Get(Guid id)
        {
            User admin = admDA.Get(id);
            if (admin != null)
            {
                return admin;
            }
            else
            {
                throw new Exception("Administrator does not exist");
            }

        }

        public User Create(User admin1)
        {
          
            if (!(ExistAdmin(admin1)) && User.CorrectData(admin1))
            {
                admDA.Create(admin1);
                admDA.Save();
                return admin1;
            }
            else
            {
                throw new Exception("The administrator already exists");
            }

        }

        private bool ExistAdmin(User admin)
        {
            return admDA.GetAll().Any(user => (user.Email == admin.Email));
        }

    }
}
