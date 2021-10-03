using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicInterface.Imports;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Imports;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Linq;
using Domain;
using Moq;

namespace WebApiTest
{
    [TestClass]
    public class ImportControllerTest
    {
        private static State activeState = new State(State.active);

        private Mock<IBugsImport<BugsImportTxt>> importBugsTxt;
        private Mock<IBugsImport<BugsImportXml>> importBugsXml;

        private Project project;
        private Bug bug;

        [TestInitialize]
        public void Setup()
        {
            importBugsTxt = new Mock<IBugsImport<BugsImportTxt>>(MockBehavior.Strict);
            importBugsXml = new Mock<IBugsImport<BugsImportXml>>(MockBehavior.Strict);

            project = new Project("Project - GXC ");
            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState);
        }

        [TestMethod]
        public void ImportBugTxt()
        {
            List<Bug> bugs = new List<Bug> { bug };

            importBugsTxt.Setup(m => m.ImportBugs("")).Returns(bugs);
            IEnumerable<BugEntryOutModel> bugsModel = bugs.Select(b => new BugEntryOutModel(b));

            ImportsController<BugsImportTxt> controller = 
                new ImportsController<BugsImportTxt>(importBugsTxt.Object);

            var result = controller.ImportBugsTxt("");
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugEntryOutModel>;

            importBugsTxt.VerifyAll();

            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(bugs.First().Id, bugsResult.First().Id);
        }

        [TestMethod]
        public void ImportBugXml()
        {
            List<Bug> bugs = new List<Bug> { bug };

            importBugsXml.Setup(m => m.ImportBugs("")).Returns(bugs);
            IEnumerable<BugEntryOutModel> bugsModel = bugs.Select(b => new BugEntryOutModel(b));

            ImportsController<BugsImportXml> controller = 
                new ImportsController<BugsImportXml>(importBugsXml.Object);

            var result = controller.ImportBugsTxt("");
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugEntryOutModel>;

            importBugsTxt.VerifyAll();

            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(bugs.First().Id, bugsResult.First().Id);
        }
    }
}
