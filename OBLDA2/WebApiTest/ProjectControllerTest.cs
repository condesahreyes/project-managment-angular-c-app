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
        private Bug bug;
        private User user;
        private static State activeState = new State(State.active);
        private List<Rol> roles;



        [TestInitialize]
        public void Setup()
        {
            roles = new List<Rol>
            {
                new Rol(Rol.tester),
                new Rol(Rol.administrator),
                new Rol(Rol.developer),
            };

            user = new User("Hernán", "Reyes", "hreyes", "contraseña",
               "hreyes.condesa@gmail.com", roles[0], 0);
            project = new Project("Project - GXC");
            bug = new Bug(project, 1, "Error de login",
              "Intento de sesión", "3.0", activeState, 0);

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

            projectLogic.Setup(m => m.GetAllBugByProject(project.Id)).Returns(bugs);
            var controller = new ProjectController(projectLogic.Object);

            var result = controller.GetAllBugsByProject(project.Id);
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugEntryOutModel>;

            projectLogic.VerifyAll();

            Assert.IsTrue(bugsOut.First().Id == bugsResult.First().Id);
        }

        [TestMethod]
        public void GetAllUsersByProject()
        {
            List<User> users = new List<User>();
            users.Add(user);
            List<UserOutModel> usersOut = UserOutModel.ListUser(users);

            projectLogic.Setup(m => m.GetAllUsersInOneProject(project.Id)).Returns(users);
            var controller = new ProjectController(projectLogic.Object);

            var result = controller.GetAllUsersByProject(project.Id);
            var okResult = result as ObjectResult;
            var usersResult = okResult.Value as IEnumerable<UserOutModel>;

            projectLogic.VerifyAll();

            Assert.IsTrue(usersOut.First().Id == usersResult.First().Id);
        }
    }
}
