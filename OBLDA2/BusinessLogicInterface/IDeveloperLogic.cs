using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IDeveloperLogic
    {
        User Create(User developer);
        List<Bug> GetAllBugs();
        User Get(string userName);
    }
}
