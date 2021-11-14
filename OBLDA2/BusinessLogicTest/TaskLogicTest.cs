using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicInterface;
using DataAccessInterface;
using BusinessLogic;
using Exceptions;
using Domain;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System;

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

        [TestMethod]
        public void GetAll()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(task);

            taskRepositoryMock.Setup(x => x.GetAll()).Returns(tasks);

            List<Task> tasksDb = taskLogic.GetAll();

            taskRepositoryMock.VerifyAll();

            Assert.IsTrue(tasks.SequenceEqual(tasksDb));
        }

        [TestMethod]
        public void GetAllByProject()
        {
            Project project = new Project();
            project.Tasks = new List<Task>();
            project.Tasks.Add(task);

            projectLogicMock.Setup(x => x.Get(It.IsAny<Guid>()))
                .Returns(project);

            List<Task> tasksDb = taskLogic.GetAllByProject(project.Id);

            taskRepositoryMock.VerifyAll();

            Assert.IsTrue(tasksDb.SequenceEqual(project.Tasks));
        }
    }
}