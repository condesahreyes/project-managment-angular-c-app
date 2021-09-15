using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogic.UserRol;
using DataAccessInterface;
using System.Linq;
using Domain;
using System;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class DeveloperLogicTest
    {
        private Mock<IRepository<User>> mockUser;
        private Mock<IRepository<Project>> mockProject;
        private Mock<IRepository<Rol>> mockRol;
        private Mock<IRepository<Bug>> mockBug;

        private List<Rol> roles;

        private User developer;

        [TestInitialize]
        public void Setup()
        {
            mockProject = new Mock<IRepository<Project>>(MockBehavior.Strict);
            mockUser = new Mock<IRepository<User>>(MockBehavior.Strict);
            mockBug = new Mock<IRepository<Bug>>(MockBehavior.Strict);

            CofnigurationMockRol();

            developer = new User("Diego", "Asadurian", "diegoAsa", "admin1234",
                "diegoasadurian@gmail.com", roles[2]);
        }

        private void CofnigurationMockRol()
        {
            mockRol = new Mock<IRepository<Rol>>(MockBehavior.Strict);

            roles = new List<Rol>
            {
                new Rol("Tester"),
                new Rol("Administrator"),
                new Rol("Developer"),
            };

            mockRol.Setup(x => x.GetAll()).Returns(roles);
        }

        [TestMethod]
        public void CreateDeveloper()
        {
            IEnumerable<User> users = new List<User>();
            mockUser.Setup(x => x.GetAll()).Returns(users);
            mockUser.Setup(x => x.Create(developer)).Returns(developer);
            
            var developerLogic = new DeveloperLogic(mockUser.Object, mockProject.Object, mockRol.Object, 
                mockBug.Object);

            User userSaved = developerLogic.Create(developer);

            mockUser.VerifyAll();

            Assert.AreEqual(developer, userSaved);
        }

        [TestMethod]
        public void GetAllBugs()
        {
            Project project = new Project("Montes Del Plata");

            project.desarrolladores.Add(developer);

            IEnumerable<Bug> bugs = new List<Bug>
            {
                new Bug(project, 1234, "Error de login", "Intento inicio de sesion", "2.0", "Activo"),
                new Bug(project, 4321, "Error de UI", "Intento inicio de sesion", "2.1", "Activo"),
            };

            List<Project> projects = new List<Project>();
            projects.Add(project);

            project.incidentes.AddRange(bugs);

            mockProject.Setup(r => r.GetAll()).Returns(projects);
            var bugLogic = new DeveloperLogic(mockUser.Object, mockProject.Object, mockRol.Object, mockBug.Object);

            List<Bug> bugsSaved = bugLogic.GetAllBugs(developer);

            mockUser.VerifyAll();
            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void GetDeveloperByName()
        {
            List<User> users = new List<User>();
            users.Add(developer);

            mockUser.Setup(r => r.GetAll()).Returns(users);
            mockUser.Setup(r => r.GetByString("diegoAsa")).Returns(developer);
            
            var developerLogic = new DeveloperLogic(mockUser.Object, mockProject.Object, mockRol.Object, 
                mockBug.Object);

            User developerSaved = developerLogic.GetByString("diegoAsa");

            Assert.IsTrue(developerSaved.UserName == "diegoAsa");
        }
    }
}
