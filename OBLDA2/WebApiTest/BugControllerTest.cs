using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using WebApi.Controllers;
using OBLDA2.Models;
using System.Linq;
using Domain;
using System;
using Moq;

namespace WebApiTest
{
    [TestClass]
    public class BugControllerTest
    {
        private static State activeState = new State(State.active);

        private Mock<IBugLogic> bugLogic;

        private Project project;
        private Bug bug;

        [TestInitialize]
        public void Setup()
        {
            bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);

            project = new Project("Project - GXC ");
            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState);
        }

        [TestMethod]
        public void AddBugTest()
        {
            BugEntryOutModel bugModelEntry = new BugEntryOutModel(bug);
            bugLogic.Setup(m => m.Create(bug)).Returns(bug);
            var controller = new BugController(bugLogic.Object);

            IActionResult result = controller.AddBug(bugModelEntry);
            var status = result as ObjectResult;
            var content = status.Value as BugEntryOutModel;

            bugLogic.VerifyAll();
            Assert.AreEqual(content.Id, bugModelEntry.Id);
        }

        [TestMethod]
        public void GetAllBugs()
        {
            List<Bug> bugs = new List<Bug>();
            bugs.Add(bug);
            List<BugEntryOutModel> bugsOut = new List<BugEntryOutModel>();

            foreach (var bug in bugs)
            {
                bugsOut.Add(new BugEntryOutModel(bug));
            }

            bugLogic.Setup(m => m.GetAll()).Returns(bugs);
            var controller = new BugController(bugLogic.Object);

            var result = controller.GetAllBugs();
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugEntryOutModel>;

            bugLogic.VerifyAll();

            Assert.IsTrue(bugsOut.First().Id == bugsResult.First().Id);
        }

        [TestMethod]
        public void GetBugIdTest()
        {
            bugLogic.Setup(m => m.Get(bug.Id)).Returns(bug);
            BugController controller = new BugController(bugLogic.Object);

            IActionResult result = controller.GetById(bug.Id);
            var okResult = result as OkObjectResult;
            var bugResult = okResult.Value as BugEntryOutModel;

            bugLogic.VerifyAll();
            Assert.AreEqual(bugResult.ToEntity(), bug);
        }


        [TestMethod]
        public void UpdateBugTest()
        {
            var updatedBug = new Bug(project, 1, "Error cierre de sesion", "Intento", "3.5", activeState);
            var bugUpdateDTO = new BugUpdateModel(updatedBug);
            BugEntryOutModel bugUpdateOutModel = new BugEntryOutModel(updatedBug);

            bugLogic.Setup(m => m.Update(bug.Id, updatedBug)).Returns(updatedBug);
            var controller = new BugController(bugLogic.Object);

            IActionResult result = controller.UpdateABug(bug.Id, bugUpdateDTO);
            var status = result as NoContentResult;

            bugLogic.VerifyAll();
            
            Assert.AreEqual(204, status.StatusCode); 
        }

        [TestMethod]
        public void DeleteBugTest()
        {
            bugLogic.Setup(m => m.Delete(bug.Id));
            BugController controller = new BugController(bugLogic.Object);

            IActionResult result = controller.Delete(bug.Id);
            var status = result as StatusCodeResult;

            bugLogic.VerifyAll();
            Assert.AreEqual(200, status.StatusCode);
        }

    }
}
