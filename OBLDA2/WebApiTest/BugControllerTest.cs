using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OBLDA2.Models;
using WebApi.Controllers;

namespace WebApiTest
{
    [TestClass]
    public class BugControllerTest
    {
        private static State activeState = new State(State.active);

        private Project project;
        private Bug bug;


        private Mock<IBugLogic> bugLogic;


        [TestInitialize]
        public void Setup()
        {
            //var adminLogic = new Mock<IAdministratorLogic>(MockBehavior.Strict);

            project = new Project("Project - GXC ");
            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", activeState);

        }

        [TestMethod]
        public void AddBugTest()
        {/*
            var bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);

            bugLogic.Setup(m => m.Create(bugDTO.toEntity())).Returns(bugDTO.toEntity());
            var controller = new BugController(bugLogic.Object);

            IActionResult result = controller.AddBug(bugDTO);
            var status = result as ObjectResult;
            var content = status.Value as BugDTO;

            bugLogic.VerifyAll();
            //Assert.AreEqual(content, new UserDTO(adminDTO.toEntity()));
            Assert.AreEqual(content.Id, bugDTO.Id); // ver que onda, porque sino comparo los objetos no me tira true
            */

        }

        [TestMethod]
        public void GetAllBugs()
        {
            var bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);

            List<Bug> list = new List<Bug>();
            list.Add(bug);
            IEnumerable<BugEntryOutModel> listOut = list.Select(b => new BugEntryOutModel(b));
            bugLogic.Setup(m => m.GetAll()).Returns(list);
            var controller = new BugController(bugLogic.Object);

            var result = controller.GetAllBugs();
            var okResult = result as OkObjectResult;
            var bugsResult = okResult.Value as List<BugEntryOutModel>;

            bugLogic.VerifyAll();

            Assert.IsTrue(listOut.SequenceEqual(bugsResult));

        }

        
        [TestMethod]
        public void GetBugIdTest()
        {
            var bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);

            bugLogic.Setup(m => m.Get(bug.Id)).Returns(bug); // me queda la duda si es DTO o sin DTO q devuelvo
            var controller = new BugController(bugLogic.Object);

            IActionResult result = controller.GetById(bug.Id);
            var okResult = result as OkObjectResult;
            var bugResult = okResult.Value as BugEntryOutModel; // creo que seria un USER

            bugLogic.VerifyAll();
            Assert.AreEqual(bugResult.ToEntity(), bug); // ver si seria asi!
        }


        [TestMethod]
        public void UpdateBugTest()
        {
            /*
            var bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);

            Guid id = new Guid();
            var updatedBug = new Bug(project, 1, "Error cierre de sesion", "Intento", "3.5", activeState);
            var bugUpdateDTO = new BugUpdateModel(updatedBug);

            bugLogic.Setup(m => m.Update(bug.Id, updatedBug)).Returns(updatedBug);
            var controller = new BugController(bugLogic.Object);

            IActionResult result = controller.UpdateABug(bug.Id, bugUpdateDTO);
            var status = result as BugDTO; ; // VER ACA QUE ONDA???

            bugLogic.VerifyAll();
            */
            //Assert.AreEqual(content.Id, bugDTO.Id); 
            //Assert.AreEqual(200, status.StatusCode); // VER ACA QUE ONDA??? LE PUSE 200 PORQUE CUANDO ACTUALIZO TIRO UN OK
        }

        [TestMethod]
        public void DeleteBugTest()
        {
            var bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);

            bugLogic.Setup(m => m.Delete(bug.Id));
            var controller = new BugController(bugLogic.Object);

            IActionResult result = controller.Delete(bug.Id);
             var status = result as StatusCodeResult;
            // var status = result as ObjectResult;

            bugLogic.VerifyAll();
            Assert.AreEqual(200, status.StatusCode); // aca lo mismo yo tiro un 200,  ver si esta bien???
        }

    }
}
