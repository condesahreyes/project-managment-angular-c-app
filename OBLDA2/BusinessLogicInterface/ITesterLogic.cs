using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface ITesterLogic
    {
        User Get(Guid id);
        User Create(User tester);
        Bug CreateBug(Bug bug);

        List<Project> GetProjectsByTester(Guid id);
        List<User> GetAll();
        List<Bug> GetAllBugs(User tester);

        void DeleteBug(int id);
        void AssignTesterToProject(Project project, User tester);
        void DeleteTesterInProject(Project project, User tester);
    }
}
