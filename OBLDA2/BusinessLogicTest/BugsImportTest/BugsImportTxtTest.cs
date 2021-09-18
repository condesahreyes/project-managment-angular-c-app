using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using BusinessLogic.Imports;
using System;
using Domain;
using Moq;

namespace BusinessLogicTest.BugsImportTest
{
    [TestClass]
    public class BugsImportTxtTest
    {
        private readonly string fileAddress = @"..\..\..\FileImport\archivoTxtBugs.txt";

        private static string activeStatus = "Activo";
        private static string doneStatus = "Resuelto";

        private static Project project = new Project(new Guid(), "Proyecto en txt");

        private List<Bug> bugsInTxt = new List<Bug>{
            new Bug(project, 1, "nombre1", "dominio1", "V 1.0", activeStatus),
            new Bug(project, 2, "nombre2", "dominio2", "V 2.0", doneStatus)
        };

        private Mock<IBugLogic> bugLogic;
        private BugsImportTxt bugsImport;

        [TestInitialize]
        public void Setup()
        {
            bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);
            bugsImport = new BugsImportTxt(bugLogic.Object);
        }

        [TestMethod]
        public void ImportBugsTxt()
        {
            bugLogic.Setup(x => x.Create(bugsInTxt[0])).Returns(bugsInTxt[0]);
            bugLogic.Setup(x => x.Create(bugsInTxt[1])).Returns(bugsInTxt[1]);

            List<Bug> bugsDesdeTxt = bugsImport.ImportBugs(fileAddress);

            bugLogic.VerifyAll();

            CollectionAssert.AreEqual(bugsDesdeTxt, bugsInTxt);
        }
    }
}
