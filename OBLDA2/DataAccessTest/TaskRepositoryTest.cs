using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using DataAccessInterface;
using System.Data.Common;
using System.Linq;
using DataAccess;
using Domain;

namespace DataAccessTest
{
    [TestClass]
    public class TaskRepositoryTest
    {
        private DbContextOptions<DataContext> _contextOptions;
        private DbConnection _connection;
        private DataContext _context;

        private ITaskRepository taskRepository;

        private const string name = "name";
        private const string otherName = "otherName";

        public TaskRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(this._connection).Options;
            this._context = new DataContext(this._contextOptions);
            this.taskRepository = new TaskRepository(this._context);
        }

        [TestInitialize]
        public void Setup()
        {
            this._connection.Open();
            this._context.Database.EnsureCreated();
        }

        [TestCleanup]
        public void CleanUp()
        {
            this._context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void GetAllTask()
        {
            List<Task> tasks = new List<Task>{
                CreateTask(name),
                CreateTask(otherName)
            };

            _context.Add(tasks.First());
            _context.Add(tasks.Last());
            _context.SaveChanges();

            List<Task> tasksDb = taskRepository.GetAll();

            CollectionAssert.AreEqual(tasks, tasksDb);
        }

        [TestMethod]
        public void GetTaskById()
        {
            Task task = CreateTask(name);

            _context.Add(task);
            _context.SaveChanges();

            Task taskDb = taskRepository.GetById(task.Id);

            Assert.AreEqual(task.Id, taskDb.Id);
        }

        private Task CreateTask(string name)
        {
            return new Task(CreateProject("OneProject"), name, 0, 0);
        }

        private Project CreateProject(string projectName)
        {
            return new Project(projectName);
        }

    }
}
