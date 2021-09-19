using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace BusinessLogicInterface
{
    public interface ITesterLogic
    {
        IEnumerable<Project> GetProjectsByTester(Guid id);

        IEnumerable<User> GetAll();
        User Get(Guid id);

        User Create(User tester);
        Bug CreateBug(Bug bug);
        List<Bug> GetAllBugs(User tester);

        void DeleteBug(int id);
    }
}
