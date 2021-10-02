using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using DataAccess.Repositories;
using Microsoft.Data.Sqlite;
using DataAccessInterface;
using System.Data.Common;
using DataAccess;
using System.Linq;
using Domain;
using System;

namespace DataAccessTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        private DbContextOptions<DataContext> _contextOptions;
        private DbConnection _connection;
        private DataContext _context;

        private IUserRepository _userRepository;

        public UserRepositoryTest()
        {
            this._connection = new SqliteConnection("Filename=:memory:");
            this._contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseSqlite(this._connection).Options;
            this._context = new DataContext(this._contextOptions);
            this._userRepository = new UserRepository(this._context);
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
            List<User> users = new List<User>{
                CreateUser(Rol.administrator),
                CreateUser(Rol.developer)
            };

            _context.Add(users.Last());
            _context.Add(users.First());
            _context.SaveChanges();

            List<User> usersDB = _userRepository.GetAllGeneric();

            CollectionAssert.AreEqual(users, usersDB);
        }

        [TestMethod]
        public void GetUser()
        {
            User user = CreateUser(Rol.tester);

            _context.Add(user);
            _context.SaveChanges();

            User userDB = _userRepository.GetById(user.Id);

            Assert.AreEqual(user.Id, userDB.Id);
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
            rols = repository.GetAllGeneric();

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
