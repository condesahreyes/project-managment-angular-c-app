using System.Collections.Generic;
using Domain;

namespace DataAccessInterface
{
    public interface IBugRepository : IRepository<Bug, int>
    {
        List<Bug> GetAll();
        Bug GetById(int id);
        Bug Update(int id, Bug bugUpdate);
    }
}
