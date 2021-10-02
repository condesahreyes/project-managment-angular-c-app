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
using System;

namespace DataAccessTest
{
    [TestClass]
    public class BugRepositoryTest
    {
        private DbContextOptions<DataContext> _contextOptions;
        private DbConnection _connection;
        private DataContext _context;

        private IBugRepository _bugRepository;
        public BugRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(this._connection).Options;
            this._context = new DataContext(this._contextOptions);
            this._bugRepository = new BugRepository(this._context);
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
        public void GetAllBugs()
        {
            List<Bug> bugs = new List<Bug>{
                CreateBug(1, State.active),
                CreateBug(2, State.done)
            };

            _context.Add(bugs.Last());
            _context.Add(bugs.First());
            _context.SaveChanges();

            List<Bug> bugsBD = _bugRepository.GetAll();

            CollectionAssert.AreEqual(bugs, bugsBD);
        }

        [TestMethod]
        public void GetBug()
        {
            Bug bug = CreateBug(1, State.active);

            _context.Add(bug);
            _context.SaveChanges();

            Bug bugDB = _bugRepository.GetById(bug.Id);

            Assert.AreEqual(bug.Id, bugDB.Id);
        }

        private Bug CreateBug(int id, string oneState)
        {
            State state = new State(oneState);
            state.Id = Guid.NewGuid();
            return new Bug(CreateProject("OneProject"), id, "", "", "", state);
        }

        private void CreateState(string state)
        {
            _context.Add(new State(State.active));
            _context.SaveChanges();
        }

        private Project CreateProject(string projectName)
        {
            return new Project(projectName);
        }

    }
}
