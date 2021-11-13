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
            bug = new Bug(project, 1, "Error de login", 
                "Intento de sesión", "3.0", activeState, 0);
        }

        [TestMethod]
        public void AddBugTest()
        {
            BugEntryOutModel bugModelEntry = new BugEntryOutModel(bug);
            bugModelEntry.CreatedBy = Guid.NewGuid().ToString();
            bugLogic.Setup(m => m.CreateByUser(bug, It.IsAny<Guid>())).Returns(bug);
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
        public void GetAllBugsByName()
        {
            List<Bug> bugs = new List<Bug>();
            bugs.Add(bug);
            List<BugEntryOutModel> bugsOut = new List<BugEntryOutModel>();

            foreach (var bug in bugs)
            {
                bugsOut.Add(new BugEntryOutModel(bug));
            }

            bugLogic.Setup(m => m.GetBugsByName(bug.Name)).Returns(bugs);
            var controller = new BugController(bugLogic.Object);

            var result = controller.GetBugsByName(bug.Name);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugEntryOutModel>;

            bugLogic.VerifyAll();

            Assert.IsTrue(bugsOut.First().Id == bugsResult.First().Id);
        }

        [TestMethod]
        public void GetAllBugsByProject()
        {
            List<Bug> bugs = new List<Bug>();
            bugs.Add(bug);
            List<BugEntryOutModel> bugsOut = new List<BugEntryOutModel>();

            foreach (var bug in bugs)
            {
                bugsOut.Add(new BugEntryOutModel(bug));
            }

            bugLogic.Setup(m => m.GetBugsByProject(bug.Project.Name)).Returns(bugs);
            var controller = new BugController(bugLogic.Object);

            var result = controller.GetBugsByProject(bug.Project.Name);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugEntryOutModel>;

            bugLogic.VerifyAll();

            Assert.IsTrue(bugsOut.First().Id == bugsResult.First().Id);
        }

        [TestMethod]
        public void GetAllBugsByState()
        {
            List<Bug> bugs = new List<Bug>();
            bugs.Add(bug);
            List<BugEntryOutModel> bugsOut = new List<BugEntryOutModel>();

            foreach (var bug in bugs)
            {
                bugsOut.Add(new BugEntryOutModel(bug));
            }

            bugLogic.Setup(m => m.GetBugsByProject(bug.State.Name)).Returns(bugs);
            var controller = new BugController(bugLogic.Object);

            var result = controller.GetBugsByProject(bug.State.Name);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugEntryOutModel>;

            bugLogic.VerifyAll();

            Assert.IsTrue(bugsOut.First().Id == bugsResult.First().Id);
        }

        [TestMethod]
        public void GetBugIdTest()
        {
            UserIdModel user = new UserIdModel(Guid.NewGuid());
            bugLogic.Setup(m => m.Get(bug.Id, user.UserId)).Returns(bug);
            BugController controller = new BugController(bugLogic.Object);

            IActionResult result = controller.GetById(bug.Id, user);
            var okResult = result as OkObjectResult;
            var bugResult = okResult.Value as BugEntryOutModel;

            bugLogic.VerifyAll();
            Assert.AreEqual(bugResult.ToEntity(), bug);
        }


        [TestMethod]
        public void UpdateBugTest()
        {
            Bug updatedBug = new Bug(project, 1, "Error cierre de sesion", "Intento", 
                "3.5", activeState, 0);
            BugUpdateModel bugUpdateDTO = new BugUpdateModel(updatedBug);
            bugUpdateDTO.UserId = Guid.NewGuid().ToString();

            bugLogic.Setup(m => m.Update(bug.Id, updatedBug, It.IsAny<Guid>())).Returns(updatedBug);
            var controller = new BugController(bugLogic.Object);

            IActionResult result = controller.UpdateABug(bug.Id, bugUpdateDTO);
            var status = result as NoContentResult;

            bugLogic.VerifyAll();
            
            Assert.AreEqual(204, status.StatusCode); 
        }

        [TestMethod]
        public void DeleteBugTest()
        {
            UserIdModel user = new UserIdModel(Guid.NewGuid());
            bugLogic.Setup(m => m.Delete(bug.Id, user.UserId));
            BugController controller = new BugController(bugLogic.Object);

            IActionResult result = controller.Delete(bug.Id, user.UserId);
            var status = result as NoContentResult;

            bugLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }

    }
}
