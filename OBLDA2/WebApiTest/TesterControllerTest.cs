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
    public class TesterControllerTest
    {
        private static State activeState = new State(State.active);

        private User tester;
        private Rol rolTester;
        private Project project;
        private Bug bug;
        private Task task;
        private string taskName;
        private int taskCost;

        private int taskDuration;

        private Mock<ITesterLogic> testerLogic;


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

            rolTester = new Rol( Rol.tester);
            tester = new User("Juan", "Gomez", "jgomez", "admin1234", 
                "gomez@gmail.com", rolTester, 0);

            project = new Project("Project - GXC ");

            bug = new Bug(project, 1, "Error de login", "Intento de sesión", 
                "3.0", activeState, 0);

            testerLogic = new Mock<ITesterLogic>(MockBehavior.Strict);
        }


        [TestMethod]
        public void AssignTesterOk()
        {
            ProjectEntryModel projectEntryModel = new ProjectEntryModel(project);

            testerLogic.Setup(x => x.AssignTesterToProject(It.IsAny<Guid>(), It.IsAny<Guid>()));

            var controller = new TesterController(testerLogic.Object);
            var result = controller.AssignTester(project.Id, tester.Id);
            var status = result as NoContentResult;

            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void DeleteTesterTest()
        {
            testerLogic.Setup(m => m.DeleteTesterInProject(project.Id, tester.Id));

            var controller = new TesterController(testerLogic.Object);

            IActionResult result = controller.DeleteTester(tester.Id, project.Id);
            var status = result as NoContentResult;

            testerLogic.VerifyAll();
            Assert.AreEqual(204, status.StatusCode);
        }

        [TestMethod]
        public void GetTotalBugsTest()
        {
            List<Bug> list = new List<Bug>();
            list.Add(bug);

            IEnumerable<BugEntryOutModel> bugsModel = new List<BugEntryOutModel>
            {
                new BugEntryOutModel(bug)
            };

            testerLogic.Setup(m => m.GetAllBugs(It.IsAny<Guid>())).Returns(list);
            TesterController controller = new TesterController(testerLogic.Object);

            var result = controller.GetAllBugsTester(It.IsAny<Guid>());
            var okResult = result as ObjectResult;
            var bugsResult = okResult.Value as IEnumerable<BugEntryOutModel>;

            testerLogic.VerifyAll();
            Assert.IsTrue(bugsModel.First().Id ==  bugsResult.First().Id);
        }

        [TestMethod]
        public void GetAllProjectsByTester()
        {
            List<Project> projects = new List<Project>();
            projects.Add(project);
            List<ProjectOutModel> projectOut = new List<ProjectOutModel>();

            foreach (var project in projects)
            {
                projectOut.Add(new ProjectOutModel(project));
            }

            testerLogic.Setup(m => m.GetAllProjects(tester.Id)).Returns(projects);
            var controller = new TesterController(testerLogic.Object);

            var result = controller.GetAllProjectsTester(tester.Id);
            var okResult = result as ObjectResult;
            var projectResult = okResult.Value as IEnumerable<ProjectOutModel>;

            testerLogic.VerifyAll();

            Assert.IsTrue(projectOut.First().Id == projectResult.First().Id);
        }

        [TestMethod]
        public void GetAll()
        {
            List<User> testers = new List<User>();
            testers.Add(tester);

            var userLogic = new Mock<IUserLogic>(MockBehavior.Strict);

            testerLogic.Setup(m => m.GetAll()).Returns(testers);
            var controller = new TesterController(testerLogic.Object);

            IActionResult result = controller.GetAll();
            var status = result as ObjectResult;
            var content = status.Value as List<UserOutModel>;

            userLogic.VerifyAll();

            Assert.IsTrue(content.Count == testers.Count);
        }


        [TestMethod]
        public void GetAllTaskByTester()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(task);
            
            List<TaskEntryOutModel> taskOut = new List<TaskEntryOutModel>();

            foreach (var task in tasks)
            {
                taskOut.Add(new TaskEntryOutModel(task));
            }

            testerLogic.Setup(m => m.GetAllTask(tester.Id)).Returns(tasks);
            var controller = new TesterController(testerLogic.Object);

            var result = controller.GetAllTask(tester.Id);
            var okResult = result as ObjectResult;
            var taskResult = okResult.Value as List<TaskEntryOutModel>;

            testerLogic.VerifyAll();

            Assert.IsTrue(taskOut.First().Name == taskResult.First().Name);
        }
    }
}
