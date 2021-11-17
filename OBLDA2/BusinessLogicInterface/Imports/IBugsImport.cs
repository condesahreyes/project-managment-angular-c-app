using System.Collections.Generic;
using Domain;

namespace BusinessLogicInterface.Imports
{
    public interface IBugsImport
    {
        List<Bug> ImportBugs(string fileAddress);
    }
}
