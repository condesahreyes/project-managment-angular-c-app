using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OBLDA2.Models;
using WebApi.Controllers;

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
            tester = new User("Juan", "Gomez", "jgomez", "admin1234", "gomez@gmail.com", rolTester);

            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState);
            project = new Project("Project - GXC ");

            testerLogic = new Mock<ITesterLogic>(MockBehavior.Strict);
        }


        [TestMethod]
        public void AssignTesterOk()
        {
            ProjectEntryModel projectEntryModel = new ProjectEntryModel(project);
            UserEntryModel testerEntryModel = new UserEntryModel(tester);

            testerLogic.Setup(x => x.AssignTesterToProject(project, tester));

            var controller = new TesterController(testerLogic.Object);
            var result = controller.AssignTester(projectEntryModel, testerEntryModel);
            var status = result as NoContentResult;

            Assert.AreEqual(204, status.StatusCode);

        }

        [TestMethod]
        public void DeleteTesterTest()
        {
            testerLogic.Setup(m => m.DeleteTesterInProject(project, tester));

            var controller = new TesterController(testerLogic.Object);

            IActionResult result = controller.DeleteTester(project, tester);
            var status = result as NoContentResult;

            testerLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }


        [TestMethod]
        public void GetTotalBugsTest()
        {
            List<Bug> list = new List<Bug>();
            list.Add(bug);

            testerLogic.Setup(m => m.GetAllBugs(tester)).Returns(list);
            TesterController controller = new TesterController(testerLogic.Object);

            var result = controller.GetAllBugs(tester.Id);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<Bug>;

            testerLogic.VerifyAll();

            Assert.IsTrue(list.SequenceEqual(bugsResult));
        }
        
    }
}
