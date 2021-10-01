using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
    public class GenericRepositoryTest
    {

        private DbContextOptions<DataContext> _contextOptions;
        private DbConnection _connection;
        private DataContext _context;

        private IRepository<User, Guid> _userRepository;

        public GenericRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(this._connection).Options;
            this._context = new DataContext(this._contextOptions);
            this._userRepository = new Repository<User, Guid>(this._context);
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
        public void CreateAdmin()
        {
            User admin = CreateUser(Rol.administrator);
            User adminSaved = _userRepository.Create(admin);

            Assert.AreEqual(admin, adminSaved);
        }

        [TestMethod]
        public void CreateDeveloper()
        {
            User developer = CreateUser(Rol.developer);
            User developerSaved = _userRepository.Create(developer);

            Assert.AreEqual(developer, developerSaved);
        }

        [TestMethod]
        public void CreateTester()
        {
            User tester = CreateUser(Rol.tester);
            User testerSaved = _userRepository.Create(tester);

            Assert.AreEqual(tester, testerSaved);
        }

        [TestMethod]
        public void GetAllUsers()
        {
            List<User> users = new List<User>{
                CreateUser(Rol.administrator),
                CreateUser(Rol.developer)
            };

            _context.Add(users.Last());
            _context.Add(users.First());
            _context.SaveChanges();

            List<User> usersDB = _userRepository.GetAll();

            CollectionAssert.AreEqual(users, usersDB);
        }

        [TestMethod]
        public void GetUserById()
        {
            User user = CreateUser(Rol.tester);

            _context.Add(user);
            _context.SaveChanges();

            User userDB = _userRepository.Get(user.Id);

            Assert.AreEqual(user.Id, userDB.Id);
        }

        [TestMethod]
        public void DeleteUser()
        {
            User user = CreateUser(Rol.tester);

            _context.Add(user);
            _context.SaveChanges();

            _userRepository.Delete(user.Id);

            List<User> usersBD = _userRepository.GetAll();

            CollectionAssert.AreEqual(usersBD, new List<User>());
        }

        [TestMethod]
        public void UpdateUser()
        {
            User user = CreateUser(Rol.tester);

            _context.Add(user);
            _context.SaveChanges();

            user.Rol = GetOneRol(Rol.developer);

            _userRepository.Update(user.Id, user);

            List<User> usersBD = _context.Users.ToList();

            Assert.IsTrue(usersBD.First().Rol.Name == user.Rol.Name);
        }

        private User CreateUser(string rol)
        {
            Rol userRol = GetOneRol(rol);

            return new User()
            {
                Name = "Hernán",
                LastName = "Reyes",
                UserName = "hreyes",
                Password = "password",
                Email = "hernan@hernan.com",
                Rol = userRol
            };
        }

        private Rol GetOneRol(string rol)
        {
            List<Rol> rols;

            _context.Add(new Rol(Rol.administrator));
            _context.Add(new Rol(Rol.tester));
            _context.Add(new Rol(Rol.developer));
            _context.SaveChanges();

            var repository = new Repository<Rol, Guid>(_context);
            rols = repository.GetAll();

            foreach (Rol oneRol in rols)
            {
                if (oneRol.Name == rol)
                {
                    return oneRol;
                }
            }

            return null;
        }

    }
}