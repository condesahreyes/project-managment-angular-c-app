using System;
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
    public class ProjectControllerTest
    {
        private ProjectEntryModel projectEntryModel;
        private Mock<IProjectLogic> projectLogic;
        private Project project;

        [TestInitialize]
        public void Setup()
        {
            project = new Project("Project - GXC ");
            projectEntryModel = new ProjectEntryModel(project);
            projectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
        }

        [TestMethod]
        public void AddProjectTest()
        {
            ProjectController controller = new ProjectController(projectLogic.Object);

            projectLogic.Setup(m => m.Create(project)).Returns(project);
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
            projectLogic.Setup(m => m.Get(project.Id)).Returns(project); // me queda la duda si es DTO o sin DTO q devuelvo
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.GetById(project.Id);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectOutModel; // creo que seria un USER

            projectLogic.VerifyAll();
            Assert.AreEqual(projectResult.Id, project.Id); // ver si seria asi!
        }


        [TestMethod]
        public void UpdateProjectTest()
        {
            Project updatedProject= new Project("Project - Globant");

            ProjectEntryModel updateProject = new ProjectEntryModel(updatedProject);

            projectLogic.Setup(m => m.Update(project.Id, updatedProject));
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.UpdateProject(project.Id, updateProject);
            var status = result as NoContentResult; // VER ACA QUE ONDA???

            projectLogic.VerifyAll();
            Assert.AreEqual(200, status.StatusCode);
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
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
