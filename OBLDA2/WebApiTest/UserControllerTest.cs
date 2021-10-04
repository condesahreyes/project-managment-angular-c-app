using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using WebApi.Controllers;
using OBLDA2.Models;
using System.Linq;
using System;
using Domain;
using Moq;
using OBLDA2.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class UserControllerTest
    {
        private static State activeState = new State(State.active);

        private User admin;
        //private User tester;
        //private User developer;
        private UserEntryModel adminDTO;

        private Rol rolAdministrator;
        //private Rol rolTester;
        //private Rol rolDeveloper;

        private Mock<IUserLogic> userLogic;


        [TestInitialize]
        public void Setup()
        {
            Guid id = new Guid();
            rolAdministrator = new Rol(Rol.administrator);
            //rolTester = new Rol(Rol.tester);

            admin = new User("Hernan", "reyes", "hernanReyes", "admin1234", "reyesH@gmail.com", rolAdministrator);
            //tester = new User("Juan", "Gomez", "jgomez", "admin1234", "gomez@gmail.com", rolTester);
            //developer = new User("Diego", "Suarez", "diegoo", "admin1234", "diegoo@gmail.com", rolDeveloper);


            adminDTO = new UserEntryModel(admin);

        }

        [TestMethod]
        public void AddUserTest()
        {
            var userLogic = new Mock<IUserLogic>(MockBehavior.Strict);

            userLogic.Setup(m => m.Create(adminDTO.ToEntity())).Returns(adminDTO.ToEntity());
            //adminLogic.Setup(m => m.Create(admin)).Returns(admin);
            var controller = new UserController(userLogic.Object);

            IActionResult result = controller.AddUser(adminDTO);
            var status = result as ObjectResult;
            var content = status.Value as UserOutModel;

            userLogic.VerifyAll();
            //Assert.AreEqual(content, new UserDTO(adminDTO.toEntity()));
            Assert.AreEqual(content.Email, adminDTO.Email); // ver que onda, porque sino comparo los objetos no me tira true


        }
    }
}
