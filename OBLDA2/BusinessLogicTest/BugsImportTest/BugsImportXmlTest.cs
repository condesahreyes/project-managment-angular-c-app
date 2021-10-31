using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogic.Imports;
using Domain;

namespace BusinessLogicTest.BugsImportTest
{
    [TestClass]
    public class BugsImportXmlTest
    {
        private readonly string fileAddress = @"..\..\..\FileImport\archivoXmlBugs.xml";

        private static Project project = new Project("Proyecto en xml");
        private static State activeState = new State(State.active);

        private List<Bug> bugsInXml = new List<Bug>{
            new Bug(project, 4561, "Error en el envío de correo", "El error se " +
                "produce cuando el usuario no tiene un correo asignado", "1.0", activeState, 55),
            new Bug(project, 9999, "Error en el envío de correo 2", "El error se produce cuando el" +
                " usuario no tiene un correo asignado 2", "1.0", activeState, 90)
        };

        private BugsImportXml bugsImport;

        [TestInitialize]
        public void Setup()
        {
            bugsImport = new BugsImportXml();
        }

        [TestMethod]
        public void ImportBugsXml()
        {
            List<Bug> bugs = bugsImport.ImportBugsXml(fileAddress);

            CollectionAssert.AreEqual(bugs, bugsInXml);
        }
    }
}
