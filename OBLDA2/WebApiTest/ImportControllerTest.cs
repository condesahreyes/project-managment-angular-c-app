using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicInterface.Imports;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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

        private Mock<IBugsImport> importBug;

        private Project project;
        private Bug bug;

        [TestInitialize]
        public void Setup()
        {
            importBug = new Mock<IBugsImport>(MockBehavior.Strict);

            project = new Project("Project - GXC ");
            bug = new Bug(project, 1, "Error de login", 
                "Intento de sesión", "3.0", activeState);
        }

        [TestMethod]
        public void ImportBugTxt()
        {
            List<Bug> bugs = new List<Bug> { bug };

            importBug.Setup(m => m.ImportBugs("")).Returns(bugs);
            IEnumerable<BugEntryOutModel> bugsModel = bugs.Select(b => new BugEntryOutModel(b));

            ImportsController controller = new ImportsController(importBug.Object);
            ImportBugModel importModel = new ImportBugModel("");
            var result = controller.ImportBugs(importModel);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugEntryOutModel>;

            importBug.VerifyAll();

            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(bugs.First().Id, bugsResult.First().Id);
        }

        [TestMethod]
        public void ImportBugXml()
        {
            List<Bug> bugs = new List<Bug> { bug };

            importBug.Setup(m => m.ImportBugs("")).Returns(bugs);
            IEnumerable<BugEntryOutModel> bugsModel = bugs.Select(b => new BugEntryOutModel(b));

            ImportsController controller = new ImportsController(importBug.Object);
            ImportBugModel importModel = new ImportBugModel("");
            var result = controller.ImportBugs(importModel);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugEntryOutModel>;

            importBug.VerifyAll();

            Assert.AreEqual(200, okResult.StatusCode);
            Assert.AreEqual(bugs.First().Id, bugsResult.First().Id);
        }
    }
}
