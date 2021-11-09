using BusinessLogicInterface.Imports;
using System.Collections.Generic;
using Newtonsoft.Json;
using OBLDA2.Models;
using System.IO;

namespace JsonImport
{
    public class JsonBugsImport : IBugsImportGeneric
    {

        public JsonBugsImport() { }

        public List<BugImportModel> ImportBugs(string fileAddress)
        {
            List<BugImportModel> bugs = new List<BugImportModel>();
            BugImportModel bug = new BugImportModel();
            FileStream stream = new FileStream(fileAddress, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(stream);
            string jsonData = streamReader.ReadToEnd();
            List<BugImportModel> bugss = DeserealizeJsonBugs(jsonData);
            bugs.Add(bug);
            return bugss;
        }

        private List<BugImportModel> DeserealizeJsonBugs(string jsonData)
        {
            return JsonConvert.DeserializeObject<List<BugImportModel>>(jsonData);
        }
    }
}
