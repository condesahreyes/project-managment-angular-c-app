using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogic.Imports;
using Domain;

namespace BusinessLogicTest.BugsImportTest
{
    [TestClass]
    public class BugsImportTxtTest
    {
        private readonly string fileAddress = @"..\..\..\FileImport\archivoTxtBugs.txt";

        private static Project project = new Project("Proyecto en txt");
        private static State stateActive = new State(State.active);
        private static State stateDone = new State(State.done);

        private List<Bug> bugsInTxt = new List<Bug>{
            new Bug(project, 1, "nombre1", "dominio1", "V 1.0", stateActive),
            new Bug(project, 2, "nombre2", "dominio2", "V 2.0", stateDone)
        };

        private BugsImportTxt bugsImport;

        [TestInitialize]
        public void Setup()
        {
            bugsImport = new BugsImportTxt();
        }

        [TestMethod]
        public void ImportBugsTxt()
        {
            List<Bug> bugsDesdeTxt = bugsImport.ImportBugsTxt(fileAddress);

            CollectionAssert.AreEqual(bugsDesdeTxt, bugsInTxt);
        }
    }
}
