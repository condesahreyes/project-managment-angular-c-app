using System.Collections.Generic;
using Domain;

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
