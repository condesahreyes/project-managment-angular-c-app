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
    public class DeveloperControllerTest
    {
        private static State activeState = new State(State.active);

        private User developer;
        private Rol rolDeveloper;
        private Project project;
        private Bug bug;

        private Mock<IDeveloperLogic> developerLogic;

        [TestInitialize]
        public void Setup()
        {
            developerLogic = new Mock<IDeveloperLogic>(MockBehavior.Strict);

            rolDeveloper = new Rol(Rol.developer);
            developer = new User("Juan", "Gomez", "jgomez", "admin1234", "gomez@gmail.com", rolDeveloper);
            
            project = new Project("Project - GXC ");
            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState);
        }

        [TestMethod]
        public void AssignDeveloperAProject()
        {
            developerLogic.Setup(x => x.AssignDeveloperToProject(project.Id, developer.Id));

            DeveloperController controller = new DeveloperController(developerLogic.Object);

            var result = controller.AssignDeveloperToProject(project.Id, developer.Id);
            var status = result as NoContentResult;

            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void DeleteDeveloperByProject()
        {
            developerLogic.Setup(m => m.DeleteDeveloperInProject(project.Id, developer.Id));

            DeveloperController controller = new DeveloperController(developerLogic.Object);

            IActionResult result = controller.DeleteDeveloperToProject(developer.Id, project.Id);
            var status = result as NoContentResult;

            developerLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void GetAllBugs()
        {
            List<Bug> list = new List<Bug>();
            list.Add(bug);

            IEnumerable<BugEntryOutModel> bugsModel = new List<BugEntryOutModel>
            {
                new BugEntryOutModel(bug)
            };

            developerLogic.Setup(m => m.GetAllBugs(It.IsAny<Guid>())).Returns(list);
            DeveloperController controller = new DeveloperController(developerLogic.Object);

            var result = controller.GetAllBugsDeveloper(It.IsAny<Guid>());
            var okResult = result as ObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugEntryOutModel>;

            developerLogic.VerifyAll();
            Assert.IsTrue(bugsModel.First().Id == bugsResult.First().Id);
        }

        [TestMethod]
        public void GetTotalBugsResolved()
        {
            bug.SolvedBy=developer;
            List<Bug> list = new List<Bug>();
            list.Add(bug);

            IEnumerable<BugEntryOutModel> bugsModel = new List<BugEntryOutModel>
            {
                new BugEntryOutModel(bug)
            };

            developerLogic.Setup(m => m.CountBugDoneByDeveloper(It.IsAny<Guid>())).Returns(1);
            DeveloperController controller = new DeveloperController(developerLogic.Object);

            var result = controller.GetCountBugsResolvedByDeveloper(It.IsAny<Guid>());
            var okResult = result as ObjectResult;

            developerLogic.VerifyAll();
            Assert.IsTrue("1" == okResult.Value.ToString());
        }

        [TestMethod]
        public void UpdateBugTest()
        {
            Bug updatedBug = new Bug(project, 1, "Error cierre de sesion", "Intento",
                "3.5", activeState);
            BugUpdateStateModel bugUpdateDTO = new BugUpdateStateModel(activeState.Name, updatedBug.Id);

            developerLogic.Setup(m => m.UpdateState(bugUpdateDTO.BugId, It.IsAny<string>(), developer.Id))
                .Returns(updatedBug);

            var controller = new DeveloperController(developerLogic.Object);

            IActionResult result = controller.UpdateStateBug(developer.Id, bugUpdateDTO);
            var status = result as NoContentResult;

            developerLogic.VerifyAll();

            Assert.AreEqual(204, status.StatusCode);
        }

    }
}
