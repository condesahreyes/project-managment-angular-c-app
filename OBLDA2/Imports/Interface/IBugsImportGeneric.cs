using Domain;
using OBLDA2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface.Imports
{
    public interface IBugsImportGeneric
    {
        List<BugImportModel> ImportBugs(string fileAddress);
    }
}
