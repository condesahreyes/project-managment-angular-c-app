using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OBLDA2.Controllers;
using OBLDA2.Models;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class ProjectControllerTest
    {
        private static State activeState = new State(State.active);

        private User admin;
        private User tester;
        private User developer;
        private Project project;
        private Bug bug;

        private ProjectEntryModel projectEntryModel;
        private UserDTO adminDTO;
        private BugDTO bugDTO;

        private Rol rolAdministrator;
        private Rol rolTester;
        private Rol rolDeveloper;


        private Mock<IProjectLogic> projectLogic;


        [TestInitialize]
        public void Setup()
        {
            Guid id = new Guid();
            rolAdministrator = new Rol(Rol.administrator);
            rolTester = new Rol(Rol.tester);

            admin = new User("Hernan", "reyes", "hernanReyes", "admin1234", "reyesH@gmail.com", rolAdministrator);
            tester = new User("Juan", "Gomez", "jgomez", "admin1234", "gomez@gmail.com", rolTester);
            developer = new User("Diego", "Suarez", "diegoo", "admin1234", "diegoo@gmail.com", rolDeveloper);
            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState);


            project = new Project("Project - GXC ");
            projectEntryModel = new ProjectEntryModel(project);
            adminDTO = new UserDTO(admin);
            bugDTO = new BugDTO(bug);
        }

        [TestMethod]
        public void AddProjectTest()
        {
            var projectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);

            projectLogic.Setup(m => m.Create(project)).Returns(project);
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.AddProject(projectEntryModel);
            ProjectOutModel projectAdded = new ProjectOutModel(project);

            var status = result as ObjectResult;
            var content = status.Value as ProjectOutModel;

            projectLogic.VerifyAll();

            Assert.AreEqual(content, projectAdded);
        }

        [TestMethod]
        public void GetAllProjects()
        {
            var projectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);

            List<Project> projects = new List<Project>();
            projects.Add(project);

            projectLogic.Setup(m => m.GetAll()).Returns(projects);
            var controller = new ProjectController(projectLogic.Object);

            var result = controller.GetAllProjects();

            IEnumerable<ProjectOutModel> projectsOut = projects.Select(p => new ProjectOutModel(p));

            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as List<ProjectOutModel>;

            projectLogic.VerifyAll();

            Assert.IsTrue(projectsOut.SequenceEqual(projectResult));
        }

        [TestMethod]
        public void GetProjectId()
        {
            var projectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);

            projectLogic.Setup(m => m.Get(project.Id)).Returns(project); // me queda la duda si es DTO o sin DTO q devuelvo
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.GetById(project.Id);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectDTO; // creo que seria un USER

            projectLogic.VerifyAll();
            Assert.AreEqual(projectResult.Id, project.Id); // ver si seria asi!
        }


        [TestMethod]
        public void UpdateProjectTest()
        {
            var projectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);

            Guid id = new Guid();
            var updatedProject= new Project(id, "Project - Globant");
            var projectUpdateDTO = new ProjectDTO(updatedProject);

            projectLogic.Setup(m => m.Update(project.Id, updatedProject));
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.UpdateProject(project.Id, projectUpdateDTO);
            var status = result as NoContentResult; // VER ACA QUE ONDA???

            projectLogic.VerifyAll();
            Assert.AreEqual(200, status.StatusCode); // VER ACA QUE ONDA??? LE PUSE 200 PORQUE CUANDO ACTUALIZO TIRO UN OK
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            var projectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);

            projectLogic.Setup(m => m.Delete(project.Id));
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.Delete(project.Id);
             var status = result as StatusCodeResult;
            // var status = result as ObjectResult;

            projectLogic.VerifyAll();
            Assert.AreEqual(200, status.StatusCode); // aca lo mismo yo tiro un 200,  ver si esta bien???
        }
    }
}
