using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using Domain;
using Moq;

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
                "admin1234", "reyesH@gmail.com", rolAdministrator);
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
    }
}
