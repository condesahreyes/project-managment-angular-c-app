using System.Collections.Generic;
using BusinessLogicInterface;
using System.Xml;
using Domain;
using System;

namespace BusinessLogic.Imports
{
    public class BugsImportXml : BugsImport
    {
        public BugsImportXml(IBugLogic bugLogic) : base(bugLogic)
        {
        }

        public List<Bug> ImportBugs(string fileAddress)
        {
            List<Bug> bugs = GetBugsString(fileAddress);

            return CreateBugs(bugs);
        }

        private List<Bug> GetBugsString(string fileAddress)
        {
            List<Bug> bugs = new List<Bug>();
            Project project=null;

            int id=0;
            string name="";
            string domain="";
            string version="";
            string state="";

            XmlReader reader = XmlReader.Create(fileAddress);

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    switch (reader.Name.ToString())
                    {
                        case "Proyecto":
                            project = new Project(new Guid(), reader.ReadString());
                            break;
                        case "Id":
                            id = Convert.ToInt32(reader.ReadString());
                            break;
                        case "Nombre":
                            name = reader.ReadString();
                            break;
                        case "Descripcion":
                            domain = reader.ReadString();
                            break;
                        case "Version":
                            version = reader.ReadString();
                            break;
                        case "Estado":
                            state = reader.ReadString();
                            bugs.Add(new Bug(project, id, name, domain, version, state));
                            break;
                    }
                }
            }

            return bugs;
        }
    }
}
