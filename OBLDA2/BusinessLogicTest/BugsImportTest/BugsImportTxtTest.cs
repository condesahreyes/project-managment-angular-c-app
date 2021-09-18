using BusinessLogic.Imports;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BusinessLogicTest.BugsImportTest
{
    [TestClass]
    public class BugsImportTxtTest
    {
        private readonly string fileAddress = @"\ArchivosImport\archivoTxtBugs.txt";

        private static string activeStatus = "Activo";
        private static string doneStatus = "Resuelto";

        private static Project project = new Project(new Guid(), "Proyecto en txt");

        private List<Bug> bugsInTxt = new List<Bug>{
            new Bug(project, 1, "nombre1", "dominio1", "V 1.0", activeStatus),
            new Bug(project, 2, "nombre2", "dominio2", "V 2.0", doneStatus)
        };

        [TestMethod]
        public void ImportBugsTxt()
        {
            List<Bug> bugsDesdeTxt = BugsImportTxt.ImportBugs(fileAddress);

            CollectionAssert.AreEqual(bugsDesdeTxt, bugsInTxt);
        }
    }
}
