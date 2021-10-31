using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using Domain;
using Moq;
using System.Collections.Generic;

namespace WebApiTest
{
    [TestClass]
    public class UserControllerTest
    {
        private User admin;
        private UserEntryModel adminDTO;

        private Rol rolAdministrator;

        [TestInitialize]
        public void Setup()
        {
            rolAdministrator = new Rol(Rol.administrator);

            admin = new User("Hernan", "reyes", "hernanReyes", 
                "admin1234", "reyesH@gmail.com", rolAdministrator, 0);
            adminDTO = new UserEntryModel(admin);
        }

        [TestMethod]
        public void AddUserTest()
        {
            var userLogic = new Mock<IUserLogic>(MockBehavior.Strict);

            userLogic.Setup(m => m.Create(adminDTO.ToEntity()))
                .Returns(adminDTO.ToEntity());
            var controller = new UserController(userLogic.Object);

            IActionResult result = controller.AddUser(adminDTO);
            var status = result as ObjectResult;
            var content = status.Value as UserOutModel;

            userLogic.VerifyAll();
            Assert.AreEqual(content.Email, adminDTO.Email);
        }

        [TestMethod]
        public void GetAllUser()
        {
            List<User> users = new List<User>
            {
                admin
            };

            var userLogic = new Mock<IUserLogic>(MockBehavior.Strict);

            userLogic.Setup(m => m.GetAll()).Returns(users);
            var controller = new UserController(userLogic.Object);

            IActionResult result = controller.GetAllUser();
            var status = result as ObjectResult;
            var content = status.Value as List<UserOutModel>;

            userLogic.VerifyAll();

            Assert.IsTrue(content.Count == users.Count);
        }

        [TestMethod]
        public void GetById()
        {
            var userLogic = new Mock<IUserLogic>(MockBehavior.Strict);

            userLogic.Setup(m => m.Get(admin.Id)).Returns(admin);
            var controller = new UserController(userLogic.Object);

            IActionResult result = controller.GetById(admin.Id);
            var status = result as ObjectResult;
            var content = status.Value as UserOutModel;

            userLogic.VerifyAll();

            Assert.IsTrue(content.Email == admin.Email);
        }
    }
}
