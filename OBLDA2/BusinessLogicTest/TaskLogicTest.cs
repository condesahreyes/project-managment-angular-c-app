using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicInterface;
using DataAccessInterface;
using BusinessLogic;
using Exceptions;
using Domain;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
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

        private double taskDuration;
        private double invalidTaskDuration;

        [TestInitialize]
        public void Setup()
        {
            this.taskName = "One Task";
            this.invalidTaskName = "";
            this.taskCost = 2000;
            this.invalidTaskCost = -10;
            this.taskDuration = 0.5;
            this.invalidTaskDuration = -0.1;

            task = new Task()
            {
                Name = taskName,
                Cost = taskCost,
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

            Task taskCreated = taskLogic.CreateTask(task);

            Mock.VerifyAll();
            Assert.AreEqual(task, taskCreated);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateTaskInvalidName()
        {
            task.Name = invalidTaskName;

            Task taskCreated = taskLogic.CreateTask(task);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateTaskInvalidCost()
        {
            task.Cost = invalidTaskCost;

            Task taskCreated = taskLogic.CreateTask(task);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateTaskInvalidDuration()
        {
            task.Duration = invalidTaskDuration;

            Task taskCreated = taskLogic.CreateTask(task);
        }
    }
}