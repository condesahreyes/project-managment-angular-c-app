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
            rolDeveloper = new Rol(Rol.developer);
            developer = new User("Juan", "Gomez", "jgomez", "admin1234", "gomez@gmail.com", rolDeveloper);

            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState);
            project = new Project("Project - GXC ");

            developerLogic = new Mock<IDeveloperLogic>(MockBehavior.Strict);
        }


        [TestMethod]
        public void AssignDeveloperAProject()
        {
            ProjectEntryModel projectEntryModel = new ProjectEntryModel(project);
            UserEntryModel testerEntryModel = new UserEntryModel(developer);

            developerLogic.Setup(x => x.AssignDeveloperToProject(project, developer));

            DeveloperController controller = new DeveloperController(developerLogic.Object);
            var result = controller.AssignDeveloper(projectEntryModel, developer.Id);
            var status = result as NoContentResult;

            Assert.AreEqual(204, status.StatusCode);

        }

        [TestMethod]
        public void DeleteDeveloperByProject()
        {
            developerLogic.Setup(m => m.DeleteDeveloperInProject(project, developer));

            DeveloperController controller = new DeveloperController(developerLogic.Object);

            IActionResult result = controller.DeleteDeveloper(developer.Id, project);
            var status = result as NoContentResult;

            developerLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void GetTotalBugs()
        {
            List<Bug> list = new List<Bug>();
            list.Add(bug);

            developerLogic.Setup(m => m.GetAllBugs(developer)).Returns(list);
            DeveloperController controller = new DeveloperController(developerLogic.Object);

            var result = controller.GetAllBugs(developer.Id);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<Bug>;

            developerLogic.VerifyAll();

            Assert.IsTrue(list.SequenceEqual(bugsResult));
        }
        
    }
}
