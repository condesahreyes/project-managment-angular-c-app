using DataAccessInterface;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface
{
    public interface IDeveloperLogic
    {
        User Create(User developer);
        List<Bug> GetAllBugs(User developer);
        User GetByString(string userName);
        Bug UpdateStateToDoneBug(Bug bug);
        Bug UpdateStateToActiveBug(Bug bug);
    }
}
