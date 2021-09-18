using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogic.Imports;
using Domain;
using System;

namespace BusinessLogicTest.BugsImportTest
{
    public class BugsImportXmlTest
    {
        private readonly string fileAddress = @"\ArchivosImport\archivoXmlBugs.txt";

        private static string activeStatus = "Activo";
        private static string doneStatus = "Resuelto";

        private static Project project = new Project(new Guid(), "Proyecto en xml");

        private List<Bug> bugsInXml = new List<Bug>{
            new Bug(project, 1, "nombre1", "dominio1", "V 1.0", activeStatus),
            new Bug(project, 2, "nombre2", "dominio2", "V 2.0", doneStatus)
        };

        [TestMethod]
        public void ImportBugsXml()
        {
            List<Bug> bugsDesdeTxt = BugsImportXml.ImportBugs(fileAddress);

            CollectionAssert.AreEqual(bugsDesdeTxt, bugsInXml);
        }
    }
}
