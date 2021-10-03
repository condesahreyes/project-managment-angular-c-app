using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicInterface.Imports
{
    public interface IBugsImport<T>
    {
        List<Bug> ImportBugs(string fileAddress);
    }
}
