using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using WebApi.Controllers;
using OBLDA2.Models;
using System.Linq;
using System;
using Domain;
using Moq;

namespace WebApiTest
{
    [TestClass]
    public class TesterControllerTest
    {
        private static State activeState = new State(State.active);

        private User tester;
        private Rol rolTester;
        private Project project;
        private Bug bug;

        private Mock<ITesterLogic> testerLogic;


        [TestInitialize]
        public void Setup()
        {
            rolTester = new Rol( Rol.tester);
            tester = new User("Juan", "Gomez", "jgomez", "admin1234", 
                "gomez@gmail.com", rolTester);

            project = new Project("Project - GXC ");

            bug = new Bug(project, 1, "Error de login", "Intento de sesi�n", 
                "3.0", activeState);

            testerLogic = new Mock<ITesterLogic>(MockBehavior.Strict);
        }


        [TestMethod]
        public void AssignTesterOk()
        {
            ProjectEntryModel projectEntryModel = new ProjectEntryModel(project);

            testerLogic.Setup(x => x.AssignTesterToProject(It.IsAny<Guid>(), It.IsAny<Guid>()));

            var controller = new TesterController(testerLogic.Object);
            var result = controller.AssignTester(Guid.NewGuid(), tester.Id);
            var status = result as NoContentResult;

            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void DeleteTesterTest()
        {
            testerLogic.Setup(m => m.DeleteTesterInProject(project.Id, tester.Id));

            var controller = new TesterController(testerLogic.Object);

            IActionResult result = controller.DeleteTester(tester.Id, project.Id);
            var status = result as NoContentResult;

            testerLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void GetTotalBugsTest()
        {
            List<Bug> list = new List<Bug>();
            list.Add(bug);

            IEnumerable<BugEntryOutModel> bugsModel = new List<BugEntryOutModel>
            {
                new BugEntryOutModel(bug)
            };

            testerLogic.Setup(m => m.GetAllBugs(It.IsAny<Guid>())).Returns(list);
            TesterController controller = new TesterController(testerLogic.Object);

            var result = controller.GetAllBugsTester(It.IsAny<Guid>());
            var okResult = result as ObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugEntryOutModel>;

            testerLogic.VerifyAll();
            Assert.IsTrue(bugsModel.First().Id ==  bugsResult.First().Id);
        }
        
    }
}
