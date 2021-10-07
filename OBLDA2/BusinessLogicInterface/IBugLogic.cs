using System;
using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        Bug Create(Bug bug);
        Bug CreateByUser(Bug bug, Guid userId);
        Bug Get(int id, Guid userId);
        Bug Update(int id, Bug bugUpdate, Guid userId);

        List<Bug> GetAll();

        void Delete(int id, Guid userId);
    }
}
