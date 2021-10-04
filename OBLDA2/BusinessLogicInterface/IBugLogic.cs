using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface
{
    public interface IBugLogic
    {
        Bug Create(Bug bug);
        Bug Get(int id);
        Bug Update(int id, Bug bugUpdate);
        Bug UpdateState(int id, string state);

        List<Bug> GetAll();

        void Delete(int id);
    }
}
