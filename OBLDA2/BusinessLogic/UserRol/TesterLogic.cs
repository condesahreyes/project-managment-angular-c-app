using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic.UserRol
{
    public class TesterLogic : ITesterLogic
    {


        private IRepository<Tester> testerDA;


        public TesterLogic(IRepository<Tester> repository)
        {
            this.testerDA = repository;
        }

        public IEnumerable<Tester> GetAll()
        {
            throw new NotImplementedException();
        }

        public object Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public object Create(Tester tester)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Project> getProjectsByTester(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bug> getBugsByTester(Guid id1, Guid id2, string name, string state)
        {
            throw new NotImplementedException();
        }

        public object CreateBug(Guid id, string v1, string v2, string v3, string v4, string v5)
        {
            throw new NotImplementedException();
        }
    }
}
