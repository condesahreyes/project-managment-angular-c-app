using System.Collections.Generic;
using BusinessLogicInterface;
using Domain;

namespace BusinessLogic.Imports
{
    public class BugsImport
    {

        private IBugLogic bugLogic = new BugLogic();

        public BugsImport(IBugLogic bugLogic)
        {
            this.bugLogic = bugLogic;
        }

        public List<Bug> CreateBugs(List<Bug> bugs)
        {
            List<Bug> bugsSaved = new List<Bug>();

            foreach(Bug bug in bugs)
            {
                bugsSaved.Add(bugLogic.Create(bug));
            }

            return bugsSaved;
        }
    }

}
