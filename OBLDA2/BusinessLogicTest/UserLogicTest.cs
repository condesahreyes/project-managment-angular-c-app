﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccessInterface;
using BusinessLogic;
using Exceptions;
using System;
using Domain;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserLogicTest
    {
        private List<User> users;

        private Mock<IRepository<Rol, Guid>> mockRol;
        private Mock<IUserRepository> mockUser;

        private List<Rol> roles;
        private User oneUser;
        private UserLogic userLogic;


        [TestInitialize]
        public void Setup()
        {
            users = new List<User>();

            mockRol = new Mock<IRepository<Rol, Guid>>(MockBehavior.Strict);
            mockUser = new Mock<IUserRepository>(MockBehavior.Strict);
            
            mockUser.Setup(x => x.GetAll()).Returns(users);

            roles = new List<Rol>
            {
                new Rol(Rol.tester),
                new Rol(Rol.administrator),
                new Rol(Rol.developer),
            };

            mockRol.Setup(x => x.GetAllGeneric()).Returns(roles);

            userLogic = new UserLogic(mockUser.Object, mockRol.Object);

            oneUser = new User("Hernán", "Reyes", "hreyes", "contraseña", 
                "hreyes.condesa@gmail.com", roles[0], 0);
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
        [ExpectedException(typeof(InvalidDataObjException))]
        public void IsNotValidName()
        {
            User invalidUser = new User("", "Reyes", "hreyes", "contraseña", 
                "hreyes.condesa@gmail.com", roles[0], 0);
            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void IsNotValidLastName()
        {
            User invalidUser = new User("Hernán", "", "hreyes", "contraseña", 
                "hreyes.condesa@gmail.com", roles[0], 0);
            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void IsNotValidUserName()
        {
            User invalidUser = new User("Hernán", "Reyes", "", "contraseña", 
                "hreyes.condesa@gmail.com", roles[0], 0);
            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void IsNotValidRol()
        {
            Rol invalidRol = new Rol("Auxiliar");
            User invalidUser = new User("Hernán", "Reyes", "hreyes", "contraseña", 
                "hreyes.condesa@gmail.com", invalidRol, 0);

            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void IsNotValidEmail()
        {
            User invalidUser = new User("Hernán", "Reyes", "hreyes", "contraseña", 
                "hreyes.condesagmail.com", roles[0], 0);

            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void IsNotValidPassword()
        {
            User invalidUser = new User("Hernán", "Reyes", "hreyes", "", 
                "hreyes.condesa@gmail.com", roles[0], 0);

            mockUser.Setup(x => x.Create(invalidUser)).Returns(invalidUser);

            User userSaved = userLogic.Create(invalidUser);
            mockUser.VerifyAll();
        }
        
        [TestMethod]
        [ExpectedException(typeof(ExistingObjectException))]
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
