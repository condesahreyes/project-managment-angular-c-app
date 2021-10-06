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

            developer = new User("Diego", "Asadurian", "diegoAsa", "admin1234",
                "diegoasadurian@gmail.com", roles[2]);
        }

        private void CofnigurationMockRol()
        {
            mockRol = new Mock<IRepository<Rol, Guid>>(MockBehavior.Strict);

            roles = new List<Rol>
            {
                new Rol(Rol.tester),
                new Rol(Rol.administrator),
                new Rol(Rol.developer),
            };

            mockRol.Setup(x => x.GetAllGeneric()).Returns(roles);
        }

        [TestMethod]
        public void GetAllBugs()
        {
            Project project = new Project("Montes Del Plata");

            project.Users.Add(developer);

            State stateActive = new State(State.active);
            List<Bug> bugs = new List<Bug>
            {
                new Bug(project, 1234, "Error de login", "Intento inicio de sesion", "2.0", stateActive),
                new Bug(project, 4321, "Error de UI", "Intento inicio de sesion", "2.1", stateActive),
            };

            List<Project> projects = new List<Project>();
            projects.Add(project);

            project.Bugs.AddRange(bugs);

            mockProject.Setup(r => r.GetAll()).Returns(projects);
            mockUser.Setup(u => u.Get(It.IsAny<Guid>())).Returns(developer);
            var bugLogic = new DeveloperLogic(mockUser.Object, mockProject.Object, mockRol.Object, mockBug.Object);

            List<Bug> bugsSaved = bugLogic.GetAllBugs(developer.Id);

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
