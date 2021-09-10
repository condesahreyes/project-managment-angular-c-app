using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using Domain;

namespace BusinessLogic.UserRol
{
    public class DeveloperLogic : IDeveloperLogic
    {
        private IDeveloperLogic userRepository;
        public DeveloperLogic(IDeveloperLogic userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Create(User developer)
        {
            throw new NotImplementedException();
        }

        public User Get(string userName)
        {
            throw new NotImplementedException();
        }

        public List<Bug> GetAllBugs()
        {
            throw new NotImplementedException();
        }
    }
}
