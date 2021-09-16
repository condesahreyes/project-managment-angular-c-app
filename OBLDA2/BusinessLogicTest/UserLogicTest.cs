using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccessInterface;
using BusinessLogic;
using System;
using Domain;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class UserLogicTest
    {
        private List<User> users;

        private Mock<IRepository<Rol>> mockRol;
        private Mock<IRepository<User>> mockUser;

        private List<Rol> roles;
        private User oneUser;
        private UserLogic userLogic;


        [TestInitialize]
        public void Setup()
        {
            users = new List<User>();

            mockRol = new Mock<IRepository<Rol>>(MockBehavior.Strict);
            mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
            
            mockUser.Setup(x => x.GetAll()).Returns(users);

            roles = new List<Rol>
            {
                new Rol("Tester"),
                new Rol("Administrator"),
                new Rol("Developer"),
            };

            mockRol.Setup(x => x.GetAll()).Returns(roles);

            userLogic = new UserLogic(mockUser.Object, mockRol.Object);

            oneUser = new User("Hernán", "Reyes", "hreyes", "contraseña", "hreyes.condesa@gmail.com", roles[0]);
        }

        [TestMethod]
        public void CreateValidUser()
        {
            mockUser.Setup(x => x.Create(oneUser)).Returns(oneUser);

            User userSaved = userLogic.Create(oneUser);

            mockUser.VerifyAll();

            Assert.AreEqual(oneUser, userSaved);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidName()
        {
            User invalidUser = new User("", "Reyes", "hreyes", "contraseña", "hreyes.condesa@gmail.com", roles[0]);
            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidLastName()
        {
            User invalidUser = new User("Hernán", "", "hreyes", "contraseña", "hreyes.condesa@gmail.com", roles[0]);
            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidUserName()
        {
            User invalidUser = new User("Hernán", "Reyes", "", "contraseña", "hreyes.condesa@gmail.com", roles[0]);
            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidRol()
        {
            Rol invalidRol = new Rol("Auxiliar");
            User invalidUser = new User("Hernán", "Reyes", "hreyes", "contraseña", "hreyes.condesa@gmail.com", invalidRol);

            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidEmail()
        {
            User invalidUser = new User("Hernán", "Reyes", "hreyes", "contraseña", "hreyes.condesagmail.com", roles[0]);

            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidPassword()
        {
            User invalidUser = new User("Hernán", "Reyes", "hreyes", "", "hreyes.condesa@gmail.com", roles[0]);

            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateExistingUser()
        {
            users.Add(oneUser);
            mockUser.Setup(x => x.GetAll()).Returns(users);

            mockUser.Setup(x => x.Create(oneUser)).Returns(oneUser);

            User userSaved = userLogic.Create(oneUser);
            mockUser.VerifyAll();
        }
    }
}
