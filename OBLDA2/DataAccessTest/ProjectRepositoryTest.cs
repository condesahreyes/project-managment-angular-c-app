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
    public class ProjectRepositoryTest
    {
        private DbContextOptions<DataContext> _contextOptions;
        private DbConnection _connection;
        private DataContext _context;

        private IProjectRepository _projectRepository;

        public ProjectRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(this._connection).Options;
            this._context = new DataContext(this._contextOptions);
            this._projectRepository = new ProjectRepository(this._context);
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
        public void GetAllUsers()
        {
            List<Project> projects = new List<Project>{
                CreateProject("Proyecto 1"),
                CreateProject("Proyecto 2")
            };

            _context.Add(projects.Last());
            _context.Add(projects.First());
            _context.SaveChanges();

            List<Project> projectDB = _projectRepository.GetAll();

            CollectionAssert.AreEqual(projects, projectDB);
        }

        [TestMethod]
        public void GetUser()
        {
            Project project = CreateProject("Projecto 1");

            _context.Add(project);
            _context.SaveChanges();

            Project userDB = _projectRepository.GetById(project.Id);

            Assert.AreEqual(project.Id, userDB.Id);
        }

        private Project CreateProject(string projectName)
        {
            return new Project()
            {
                Name = projectName
            };
        }

    }
}
