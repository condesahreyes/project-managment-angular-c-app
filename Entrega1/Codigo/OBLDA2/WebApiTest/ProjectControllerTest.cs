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
            project = new Project("Project - GXC");

            projectEntryModel = new ProjectEntryModel(project);
            projectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);
        }

        [TestMethod]
        public void AddProjectTest()
        {
            projectLogic.Setup(m => m.Create(It.IsAny<Project>())).Returns(project);
            ProjectController controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.AddProject(projectEntryModel);
            ProjectOutModel projectAdded = new ProjectOutModel(project);

            var status = result as ObjectResult;
            var content = status.Value as ProjectOutModel;

            projectLogic.VerifyAll();

            Assert.IsTrue(content.Name == projectAdded.Name);
        }

        [TestMethod]
        public void GetAllProjects()
        {
            List<Project> projects = new List<Project>();
            projects.Add(project);

            projectLogic.Setup(m => m.GetAll()).Returns(projects);
            var controller = new ProjectController(projectLogic.Object);

            var result = controller.GetAllProjects();

            List<ProjectOutModel> projectsOut = new List<ProjectOutModel>();

            foreach (Project project in projects)
            {
                projectsOut.Add(new ProjectOutModel(project));
            }

            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as IEnumerable<ProjectOutModel>;

            projectLogic.VerifyAll();

            Assert.IsTrue(projectsOut.First().Id == projectResult.First().Id);
        }

        [TestMethod]
        public void GetProjectId()
        {
            projectLogic.Setup(m => m.Get(project.Id)).Returns(project);
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.GetById(project.Id);
            var okResult = result as OkObjectResult;
            var projectResult = okResult.Value as ProjectOutModel;

            projectLogic.VerifyAll();
            Assert.AreEqual(projectResult.Id, project.Id);
        }


        [TestMethod]
        public void UpdateProjectTest()
        {
            Project updatedProject= new Project("Project - Globant");

            ProjectEntryModel updateProject = new ProjectEntryModel(updatedProject);

            projectLogic.Setup(m => m.Update(It.IsAny<Guid>(), It.IsAny<Project>())).Returns(updatedProject);
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.UpdateProject(project.Id, updateProject);
            var status = result as NoContentResult;

            projectLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void DeleteProjectTest()
        {
            projectLogic.Setup(m => m.Delete(project.Id));
            var controller = new ProjectController(projectLogic.Object);

            IActionResult result = controller.Delete(project.Id);
             var status = result as NoContentResult;

            projectLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }
    }
}
