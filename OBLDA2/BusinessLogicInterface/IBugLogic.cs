using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        Bug Create(Bug bug);
        Bug CreateByUser(Bug bug, Guid userId);
        Bug Get(int id, Guid userId);
        Bug Update(int id, Bug bugUpdate, Guid userId);

        List<Bug> GetAll();
        List<Bug> GetBugsByName(string name);
        List<Bug> GetBugsByState(string state);
        List<Bug> GetBugsByProject(string project);

        void Delete(int id, Guid userId);
    }
}
