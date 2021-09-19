using System;
using System.Collections.Generic;
using System.Text;
using Domain;

namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        Bug Create(Bug bug);
        void Delete(int id);
        IEnumerable<Bug> GetAll();
        Bug Update(int id, Bug bugUpdate);
        Bug Get(int id);

    }
}
