using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface.Imports
{
    interface IBugsImportTxt
    {
        List<Bug> ImportBugs(string fileAddress);
    }
}
