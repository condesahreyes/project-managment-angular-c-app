using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface.Imports
{
    public interface IBugsImport
    {
        List<Bug> ImportBugs(string fileAddress);
    }
}
