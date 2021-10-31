using System.Collections.Generic;
using Exceptions;
using System.IO;
using Domain;
using System;

namespace BusinessLogic.Imports
{
    public class BugsImportTxt
    {
        private const string invalidRoute = "Error, it´s not a valid route";
        public BugsImportTxt() { }

        public List<Bug> ImportBugsTxt(string fileAddress)
        {
            if (!File.Exists(fileAddress))
                throw new InvalidDataObjException(invalidRoute);

            List<string> bugsString = GetBugsString(fileAddress);

            return GetBugsByStrings(bugsString);
        }

        private List<string> GetBugsString(string fileAddress)
        {
            StreamReader file = new StreamReader(fileAddress);

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
            string stateString = bugString.Substring(254, 8);
            string durationString = bugString.Substring(262);

            int id = Convert.ToInt32(idString);
            int duration = Convert.ToInt32(durationString);

            Project project = new Project(projectName);
            State state = new State(stateString);

            return new Bug(project, id, name, domain, version, state, duration);
        }

    }
}
