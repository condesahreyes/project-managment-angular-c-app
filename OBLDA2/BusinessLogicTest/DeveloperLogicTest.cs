using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using BusinessLogic.UserRol;
using System.Linq;
using Domain;
using System;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeveloperLogicTest
    {
        private Mock<IUserLogic> mockUser;
        private Mock<IProjectLogic> mockProject;
        private Mock<IBugLogic> mockBug;

        private IDeveloperLogic developerLogic;

        private User developer;

        [TestInitialize]
        public void Setup()
        {
            mockProject = new Mock<IProjectLogic>(MockBehavior.Strict);
            mockUser = new Mock<IUserLogic>(MockBehavior.Strict);
            mockBug = new Mock<IBugLogic> (MockBehavior.Strict);

            developerLogic = new DeveloperLogic(mockUser.Object, 
                mockProject.Object, mockBug.Object);

            developer = new User("Diego", "Asadurian", "diegoAsa", "admin1234",
                "diegoasadurian@gmail.com", new Rol(Rol.developer), 0);
        }

        [TestMethod]
        public void GetAllTesters()
        {
            List<User> list = new List<User>();
            list.Add(developer);
            mockUser.Setup(x => x.GetAll()).Returns(list);

            List<User> ret = developerLogic.GetAll();
            mockUser.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void GetAllBugs()
        {
            Project project = new Project("Montes Del Plata");

            project.Users.Add(developer);

            State stateActive = new State(State.active);
            List<Bug> bugs = new List<Bug>
            {
                new Bug(project, 1234, "Error de login",
                "Intento inicio de sesion", "2.0", stateActive, 0),
                new Bug(project, 4321, "Error de UI",
                "Intento inicio de sesion", "2.1", stateActive, 0),
            };

            List<Project> projects = new List<Project>();
            projects.Add(project);

            project.Bugs.AddRange(bugs);

            mockProject.Setup(r => r.GetAll()).Returns(projects);
            mockUser.Setup(r => r.Get(developer.Id)).Returns(developer);

            List<Bug> bugsSaved = developerLogic.GetAllBugs(developer.Id);

            mockUser.VerifyAll();
            mockProject.VerifyAll();

            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void CountsBugDoneByDeveloper()
        {
            List<Project> projects = new List<Project>();
            Project project = new Project(Rol.developer);
            projects.Add(project);
            project.Users.Add(developer);

            project.Bugs = new List<Bug> {
                new Bug(){ SolvedBy = developer } 
            };

            mockProject.Setup(x => x.GetAll()).Returns(projects);
            mockUser.Setup(r => r.Get(developer.Id)).Returns(developer);

            int countResolved = developerLogic.CountBugDoneByDeveloper(developer.Id);

            mockProject.VerifyAll();
            mockUser.VerifyAll();
            Assert.IsTrue(1 == countResolved);
        }

        [TestMethod]
        public void GetAllProject()
        {
            List<Project> projects = new List<Project>();
            Project project = new Project(Rol.developer);
            projects.Add(project);
            project.Users.Add(developer);

            mockProject.Setup(x => x.GetAll()).Returns(projects);

            List<Project> projectsByDeveloper = developerLogic.GetAllProjects(developer.Id);

            mockProject.VerifyAll();
            Assert.IsTrue(projectsByDeveloper.SequenceEqual(projects));
        }

        [TestMethod]
        public void AssignUserToProject()
        {
            mockProject.Setup(x => x.AssignUser(It.IsAny<Guid>(), ref developer));
            mockUser.Setup(x => x.Get(It.IsAny<Guid>())).Returns(developer);

            developerLogic.AssignDeveloperToProject(It.IsAny<Guid>(), developer.Id);
            mockProject.VerifyAll();

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void DeleteUserToProject()
        {
            mockProject.Setup(x => x.DeleteUser(It.IsAny<Guid>(), ref developer));
            mockUser.Setup(x => x.Get(It.IsAny<Guid>())).Returns(developer);

            developerLogic.DeleteDeveloperInProject(It.IsAny<Guid>(), developer.Id);
            mockProject.VerifyAll();

            Assert.IsTrue(true);
        }
    }
}
