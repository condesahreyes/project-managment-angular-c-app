using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using BusinessLogic.Imports;
using Domain;
using Moq;

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
                "produce cuando el usuario no tiene un correo asignado", "1.0", activeState),
            new Bug(project, 9999, "Error en el envío de correo 2", "El error se produce cuando el" +
                " usuario no tiene un correo asignado 2", "1.0", activeState)
        };

        private Mock<IBugLogic> bugLogic;
        private BugsImportXml bugsImport;

        [TestInitialize]
        public void Setup()
        {
            bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);
            bugsImport = new BugsImportXml(bugLogic.Object);
        }

        [TestMethod]
        public void ImportBugsXml()
        {
            bugLogic.Setup(x => x.Create(bugsInXml[0])).Returns(bugsInXml[0]);
            bugLogic.Setup(x => x.Create(bugsInXml[1])).Returns(bugsInXml[1]);

            List<Bug> bugs = bugsImport.ImportBugs(fileAddress);

            CollectionAssert.AreEqual(bugs, bugsInXml);
        }
    }
}
