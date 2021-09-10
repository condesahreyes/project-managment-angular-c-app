using BusinessLogic.UserRol;
using BusinessLogicInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTest
{
    [TestClass]
    public class DeveloperLogicTest
    {
        private Mock<IDeveloperLogic> mock;
        private Rol developerRol;
        private User developer;

        [TestInitialize]
        public void Setup()
        {
            Guid id = new Guid();
            mock = new Mock<IDeveloperLogic>(MockBehavior.Strict);
            developerRol = new Rol(id, "Developer");

            developer = new User(id, "Diego", "Asadurian", "diegoAsa", "admin1234",
                "diegoasadurian@gmail.com", developerRol);
        }

        [TestMethod]
        public void CreateDeveloper()
        {

            mock.Setup(x => x.Create(developer)).Returns(developer);

            var developerLogic = new DeveloperLogic(mock.Object);

            User userSaved = developerLogic.Create(developer);

            mock.VerifyAll();

            Assert.AreEqual(developer, userSaved);
        }

        [TestMethod]
        public void GetAllBugs()
        {
            Project project = new Project("Montes Del Plata");

            project.desarrolladores.Add(developer);

            var bugs = new List<Bug>
            {
                new Bug(project, 1234, "Error de login", "Intento inicio de sesion", "2.0", "Activo"),
                new Bug(project, 4321, "Error de UI", "Intento inicio de sesion", "2.1", "Activo"),
            };

            project.incidentes.AddRange(bugs);

            mock.Setup(r => r.GetAllBugs()).Returns(bugs);
            var bugLogic = new DeveloperLogic(mock.Object);

            List<Bug> bugsSaved = bugLogic.GetAllBugs();

            mock.VerifyAll();
            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void GetDeveloperByName()
        {
            mock.Setup(r => r.Get("diegoAsa")).Returns(developer);
            var developerLogic = new DeveloperLogic(mock.Object);

            User developerSaved = developerLogic.Get("diegoAsa");

            mock.VerifyAll();
            Assert.IsTrue(developerSaved.Name == "diegoAsa");
        }

    }
}
