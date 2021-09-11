using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace BusinessLogicInterface
{
    public interface ITesterLogic
    {
        IEnumerable<Project> getProjectsByTester(Guid id);
        IEnumerable<Bug> getBugsByTester(Guid id, Guid id2, string name, string state);
    }
}
