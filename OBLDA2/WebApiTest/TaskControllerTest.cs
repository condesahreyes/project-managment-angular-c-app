using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using WebApi.Controllers;
using OBLDA2.Models;
using Domain;
using Moq;

namespace WebApiTest
{
    [TestClass]
    public class TaskControllerTest
    {
        private Mock<ITaskLogic> taskLogicMock;
        private TaskController taskController;

        private Task task;
        private TaskEntryOutModel taskModel;

        private string taskName;
        private int taskCost;

        private double taskDuration;

        [TestInitialize]
        public void Setup()
        {
            this.taskName = "One Task";
            this.taskCost = 2000;
            this.taskDuration = 0.5;

            task = new Task()
            {
                Name = taskName,
                Cost = taskCost,
                Duration = taskDuration,
                Project = new Project("project")
            };

            taskModel = new TaskEntryOutModel()
            {
                Name = task.Name,
                Cost = task.Cost,
                Duration = task.Duration,
                Project = task.Project.Name
            };

            this.taskLogicMock = new Mock<ITaskLogic>(MockBehavior.Strict);
            this.taskController = new TaskController(taskLogicMock.Object);
        }

        [TestMethod]
        public void PostTask()
        {
            taskLogicMock.Setup(x => x.Create(It.IsAny<Task>())).Returns(task);
            IActionResult result = taskController.CreateTask(taskModel);
            var okResult = result as OkObjectResult;
            var taskResult = okResult.Value as TaskEntryOutModel;

            taskLogicMock.VerifyAll();
            Assert.AreEqual(taskResult.ToEntity(), task);
        }

    }
}