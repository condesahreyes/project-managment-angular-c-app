using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using BusinessLogic.Imports;
using Domain;
using System;
using Moq;

namespace BusinessLogicTest.BugsImportTest
{
    [TestClass]
    public class BugsImportXmlTest
    {
        private readonly string fileAddress = @"..\..\..\FileImport\archivoXmlBugs.xml";

        private static Project project = new Project(new Guid(), "Proyecto en xml");

        private List<Bug> bugsInXml = new List<Bug>{
            new Bug(project, 1, "Error en el envío de correo 2", "El error se " +
                "produce cuando el usuario no tiene un correo asignado", "1.0", "Activo"),
            new Bug(project, 2, "Error en el envío de correo 2", "El error se produce cuando el" +
                " usuario no tiene un correo asignado 2", "1.0", "Activo")
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

            List<Bug> bugsDesdeTxt = bugsImport.ImportBugs(fileAddress);

            bugLogic.VerifyAll();

            CollectionAssert.AreEqual(bugsDesdeTxt, bugsInXml);
        }
    }
}
