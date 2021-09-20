using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
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
        private Mock<IUserLogic> mockUser;
        private Mock<IProjectLogic> mockProject;
        private Mock<IRepository<Rol, Guid>> mockRol;
        private Mock<IBugLogic> mockBug;

        private List<Rol> roles;

        private User developer;

        [TestInitialize]
        public void Setup()
        {
            mockProject = new Mock<IProjectLogic>(MockBehavior.Strict);
            mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockBug = new Mock<IBugLogic> (MockBehavior.Strict);

            CofnigurationMockRol();

            developer = new User(new Guid(), "Diego", "Asadurian", "diegoAsa", "admin1234",
                "diegoasadurian@gmail.com", roles[2]);
        }

        private void CofnigurationMockRol()
        {
            mockRol = new Mock<IRepository<Rol, Guid>>(MockBehavior.Strict);

            roles = new List<Rol>
            {
                new Rol(new Guid(), Rol.tester),
                new Rol(new Guid(), Rol.administrator),
                new Rol(new Guid(), Rol.developer),
            };

            mockRol.Setup(x => x.GetAll()).Returns(roles);
        }

        [TestMethod]
        public void CreateDeveloper()
        {
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
            Project project = new Project(new Guid(), "Montes Del Plata");

            project.developers.Add(developer);

            State stateActive = new State(State.active);
            List<Bug> bugs = new List<Bug>
            {
                new Bug(project, 1234, "Error de login", "Intento inicio de sesion", "2.0", stateActive),
                new Bug(project, 4321, "Error de UI", "Intento inicio de sesion", "2.1", stateActive),
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
            mockUser.Setup(r => r.Get(developer.Id)).Returns(developer);
            
            var developerLogic = new DeveloperLogic(mockUser.Object, mockProject.Object, mockRol.Object, 
                mockBug.Object);

            User developerSaved = developerLogic.GetByString("diegoAsa");

            Assert.IsTrue(developerSaved.UserName == "diegoAsa");
        }
    }
}
