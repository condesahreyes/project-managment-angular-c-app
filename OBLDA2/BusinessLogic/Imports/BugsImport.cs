using BusinessLogicInterface.Imports;
using System.Collections.Generic;
using BusinessLogicInterface;
using System.IO;
using Exceptions;
using Domain;

namespace BusinessLogic.Imports
{
    public class BugsImport : IBugsImport
    {
        private const string txtExtension = ".txt";
        private const string xmlExtension = ".xml";
        private const string invalidFormat = "Error, it´s not a valid format";

        private IBugLogic bugLogic;

        public BugsImport(IBugLogic bugLogic)
        {
            this.bugLogic = bugLogic;
        }

        public BugsImport() { }

        public List<Bug> CreateBugs(List<Bug> bugs)
        {
            List<Bug> bugsSaved = new List<Bug>();

            foreach (Bug bug in bugs)
            {
                bugsSaved.Add(bugLogic.Create(bug));
            }

            return bugsSaved;
        }

        public List<Bug> ImportBugs(string fileAddress)
        {
            List<Bug> bugsToSaved = new List<Bug>();

            if (Path.GetExtension(fileAddress).ToLower() == txtExtension)
            {
                BugsImportTxt txt = new BugsImportTxt();
                bugsToSaved = txt.ImportBugsTxt(fileAddress);
            }
            else if (Path.GetExtension(fileAddress).ToLower() == xmlExtension)
            {
                BugsImportXml xml = new BugsImportXml();
                bugsToSaved = xml.ImportBugsXml(fileAddress);
            }
            else
            {
                throw new InvalidDataObjException(invalidFormat);
            }

            return CreateBugs(bugsToSaved);
        }
    }

}
