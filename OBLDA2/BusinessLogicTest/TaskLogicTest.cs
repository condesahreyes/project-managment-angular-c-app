using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicInterface;
using DataAccessInterface;
using BusinessLogic;
using Exceptions;
using Domain;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TaskLogicTest
    {
        private ITaskLogic taskLogic;
        private Mock<ITaskRepository> taskRepositoryMock;
        private Mock<IProjectLogic> projectLogicMock;

        private Task task;

        private string taskName;
        private string invalidTaskName;
        private int taskCost;
        private int invalidTaskCost;

        private int taskDuration;
        private int invalidTaskDuration;

        [TestInitialize]
        public void Setup()
        {
            this.taskName = "One Task";
            this.invalidTaskName = "";
            this.taskCost = 2000;
            this.invalidTaskCost = -10;
            this.taskDuration = 5;
            this.invalidTaskDuration = -1;

            task = new Task()
            {
                Name = taskName,
                Price = taskCost,
                Duration = taskDuration,
                Project = new Project()
            };

            taskRepositoryMock = new Mock<ITaskRepository>(MockBehavior.Strict);
            projectLogicMock = new Mock<IProjectLogic>(MockBehavior.Strict);
            taskLogic = new TaskLogic(taskRepositoryMock.Object, projectLogicMock.Object);
        }

        [TestMethod]
        public void CreateTaskOk()
        {
            taskRepositoryMock.Setup(x => x.Create(task)).Returns(task);
            projectLogicMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>()))
                .Returns(It.IsAny<Project>());

            Task taskCreated = taskLogic.Create(task);

            taskRepositoryMock.VerifyAll();
            Assert.AreEqual(task, taskCreated);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateTaskInvalidName()
        {
            task.Name = invalidTaskName;

            Task taskCreated = taskLogic.Create(task);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateTaskInvalidCost()
        {
            task.Price = invalidTaskCost;

            Task taskCreated = taskLogic.Create(task);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateTaskInvalidDuration()
        {
            task.Duration = invalidTaskDuration;

            Task taskCreated = taskLogic.Create(task);
        }
    }
}