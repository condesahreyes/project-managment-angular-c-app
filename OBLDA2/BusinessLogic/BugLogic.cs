using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
    public class BugLogic
    {
        private IRepository<Bug> bugRepository;
        public BugLogic(IRepository<Bug> bugRepository)
        {
            this.bugRepository = bugRepository;
        }

        public Bug Create(Bug bug)
        {
            throw new NotImplementedException();
        }

        public void Delete(Bug bug)
        {
            throw new NotImplementedException();
        }

        public List<Bug> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Bug bug, Bug bugUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
