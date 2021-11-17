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
    public class TaskControllerTest
    {
        private Mock<ITaskLogic> taskLogicMock;
        private TaskController taskController;

        private Task task;
        private TaskEntryOutModel taskModel;

        private string taskName;
        private int taskCost;

        private int taskDuration;

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

            taskModel = new TaskEntryOutModel()
            {
                Name = task.Name,
                Cost = task.Price,
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

        [TestMethod]
        public void GetAllTasks() { 

            List<Task> tasks = new List<Task>();
            tasks.Add(task);

            taskLogicMock.Setup(m => m.GetAll()).Returns(tasks);
            var controller = new TaskController(taskLogicMock.Object);

            var result = controller.GetAllTask();

            List<TaskEntryOutModel> tasksOut = new List<TaskEntryOutModel>();

            foreach (Task task in tasks)
            {
                tasksOut.Add(new  TaskEntryOutModel(task));
            }

            var okResult = result as OkObjectResult;
            var taskResult = okResult.Value as IEnumerable<TaskEntryOutModel>;

            taskLogicMock.VerifyAll();

            Assert.IsTrue(tasksOut.First().Name == taskResult.First().Name);
        }

        [TestMethod]
        public void GetAllTasksByProject()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(task);
            List<TaskEntryOutModel> taskOut = new List<TaskEntryOutModel>();

            foreach (var task in tasks)
            {
                taskOut.Add(new TaskEntryOutModel(task));
            }

            taskLogicMock.Setup(m => m.GetAllByProject(task.Id)).Returns(tasks);
            var controller = new TaskController(taskLogicMock.Object);

            var result = controller.GetAllTaskByProject(task.Id);
            var okResult = result as OkObjectResult;
            var taskResult = okResult.Value as List<TaskEntryOutModel>;

            taskLogicMock.VerifyAll();

            Assert.IsTrue(taskOut.First().Name == taskResult.First().Name);
        }
    }
}