using BusinessLogicInterface.Imports;
using System.Collections.Generic;
using BusinessLogicInterface;
using Domain;
using System;

namespace BusinessLogic.Imports
{
    public class BugsImportTxt : BugsImport, IBugsImport<BugsImportTxt>
    {
        public BugsImportTxt(IBugLogic bugLogic) : base(bugLogic) { }

        public BugsImportTxt() : base() { }

        public List<Bug> ImportBugs(string fileAddress)
        {
            List<string> bugsString = GetBugsString(fileAddress);
            List<Bug> bugs = GetBugsByStrings(bugsString);

            return CreateBugs(bugs);
        }

        private List<string> GetBugsString(string fileAddress)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(fileAddress);

            List<string> bugs = new List<string>();

            string line;

            while ((line = file.ReadLine()) != null)
            {
                bugs.Add(line);
            }

            file.Close();

            return bugs;
        }

        private List<Bug> GetBugsByStrings(List<string> bugsStrings)
        {
            List<Bug> bugs = new List<Bug>();

            foreach (string bugString in bugsStrings)
            {
                bugs.Add(GetBugByString(bugString));
            }

            return bugs;
        }

        private Bug GetBugByString(string bugString)
        {
            string projectName = bugString.Substring(0, 29);
            string idString = bugString.Substring(30, 4);
            string name = bugString.Substring(34, 60);
            string domain = bugString.Substring(94, 150);
            string version = bugString.Substring(244, 10);
            string stateString = bugString.Substring(254);

            int id = Convert.ToInt32(idString);

            Project project = new Project(projectName);
            State state = new State(stateString);

            return new Bug(project, id, name, domain, version, state);
        }

    }
}
