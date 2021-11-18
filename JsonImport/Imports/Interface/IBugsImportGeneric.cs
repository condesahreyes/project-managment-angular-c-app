using OBLDA2.Models;
using System.Collections.Generic;

namespace BusinessLogicInterface.Imports
{
    public interface IBugsImportGeneric
    {
        List<BugImportModel> ImportBugs(string fileAddress);
    }
}
