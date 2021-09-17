using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace BusinessLogicInterface
{
    public interface ITesterLogic
    {
        IEnumerable<Project> getProjectsByTester(Guid id);
    }
}
