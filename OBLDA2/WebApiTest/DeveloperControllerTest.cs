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
        private Task task;
        private string taskName;
        private int taskCost;

        private int taskDuration;

        private Mock<IDeveloperLogic> developerLogic;

        [TestInitialize]
        public void Setup()
        {
            this.taskName = "One Task";
            this.taskCost = 2000;
            this.taskDuration = 1;

            task = new Task()
            {
                Name = taskName,
                Price = taskCost,
                Duration = taskDuration,
                Project = new Project("project")
            };

            developerLogic = new Mock<IDeveloperLogic>(MockBehavior.Strict);

            rolDeveloper = new Rol(Rol.developer);
            developer = new User("Juan", "Gomez", "jgomez", "admin1234", "gomez@gmail.com", rolDeveloper, 0);
            
            project = new Project("Project - GXC ");
            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState, 0);
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
                "3.5", activeState, 0);
            BugUpdateStateModel bugUpdateDTO = new BugUpdateStateModel(activeState.Name, updatedBug.Id);

            developerLogic.Setup(m => m.UpdateState(bugUpdateDTO.BugId, It.IsAny<string>(), developer.Id))
                .Returns(updatedBug);

            var controller = new DeveloperController(developerLogic.Object);

            IActionResult result = controller.UpdateStateBug(developer.Id, bugUpdateDTO);
            var status = result as NoContentResult;

            developerLogic.VerifyAll();

            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void GetAllTaskByDeveloper()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(task);

            List<TaskEntryOutModel> taskOut = new List<TaskEntryOutModel>();

            foreach (var task in tasks)
            {
                taskOut.Add(new TaskEntryOutModel(task));
            }

            developerLogic.Setup(m => m.GetAllTask(developer.Id)).Returns(tasks);
            var controller = new DeveloperController(developerLogic.Object);

            var result = controller.GetAllTask(developer.Id);
            var okResult = result as ObjectResult;
            var taskResult = okResult.Value as List<TaskEntryOutModel>;

            developerLogic.VerifyAll();

            Assert.IsTrue(taskOut.First().Name == taskResult.First().Name);
        }


        [TestMethod]
        public void GetAll()
        {
            List<User> developers = new List<User>();
            developers.Add(developer);

            var userLogic = new Mock<IUserLogic>(MockBehavior.Strict);

            developerLogic.Setup(m => m.GetAll()).Returns(developers);
            var controller = new DeveloperController(developerLogic.Object);

            IActionResult result = controller.GetAll();
            var status = result as ObjectResult;
            var content = status.Value as List<UserOutModel>;

            userLogic.VerifyAll();

            Assert.IsTrue(content.Count == developers.Count);
        }

        [TestMethod]
        public void GetAllProjectsByDeveloper()
        {
            List<Project> projects = new List<Project>();
            projects.Add(project);
            List<ProjectOutModel> projectOut = new List<ProjectOutModel>();

            foreach (var project in projects)
            {
                projectOut.Add(new ProjectOutModel(project));
            }

            developerLogic.Setup(m => m.GetAllProjects(developer.Id)).Returns(projects);
            var controller = new DeveloperController(developerLogic.Object);

            var result = controller.GetAllProjectsDeveloper(developer.Id);
            var okResult = result as ObjectResult;
            var projectResult = okResult.Value as List<ProjectOutModel>;

            developerLogic.VerifyAll();

            Assert.IsTrue(projectOut.First().Id == projectResult.First().Id);
        }

    }
}
