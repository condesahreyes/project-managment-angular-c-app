using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using BusinessLogic;
using System.Linq;
using Exceptions;
using System;
using Domain;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProjectLogicTest
    {

        private Mock<IProjectRepository> mockProjectRepository;
        private Mock<IUserLogic> userMock;

        private ProjectLogic projectLogic;
        private Project project;

        private User developer;
        private User tester;

        private Rol rolDeveloper;
        private Rol rolTester;

        [TestInitialize]
        public void Setup()
        {
            mockProjectRepository = new Mock<IProjectRepository>(MockBehavior.Strict);
            userMock = new Mock<IUserLogic>(MockBehavior.Strict);

            this.projectLogic = new ProjectLogic(mockProjectRepository.Object, userMock.Object);

            project = new Project("Project - Pineapple ");

            rolTester = new Rol(Rol.tester);
            rolDeveloper = new Rol(Rol.developer);

            tester = new User("Fiorella", "Petrone", "fioPetro", "fio1245",
                "fiore@gmail.com", rolTester, 0);
            developer = new User("Juan", "Gomez", "juanG", "juann245",
                "juan@gmail.com", rolDeveloper, 10);
        }

        [TestMethod]
        public void CreateProjectOk()
        {
            List<Project> list = new List<Project>();
            list.Add(project);
            mockProjectRepository.Setup(x => x.Create(project)).Returns(project);
            mockProjectRepository.Setup(x => x.GetAll()).Returns(list);
            var projectCreated = projectLogic.Create(project);
            mockProjectRepository.VerifyAll();
            Assert.AreEqual(project, projectCreated);
        }

        [TestMethod]
        public void GetAllProjects()
        {
            List<Project> list = new List<Project>();
            list.Add(project);
            mockProjectRepository.Setup(x => x.GetAll()).Returns(list);
            mockProjectRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            List<Project> ret = projectLogic.GetAll();

            mockProjectRepository.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void GetProjectByIdOk()
        {
            mockProjectRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var ret = projectLogic.Get(project.Id);

            mockProjectRepository.VerifyAll();

            Assert.IsTrue(ret.Equals(project));
        }

        [ExpectedException(typeof(NoObjectException))]
        [TestMethod]
        public void GetProjectByIdFail()
        {
            Guid id = Guid.NewGuid();
            Project projectoEmpty = null;
            mockProjectRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(projectoEmpty);
            Project project = projectLogic.Get(id);
        }

        [TestMethod]
        public void RemoveProjectOk()
        {
            List<Project> list = new List<Project>();
            list.Add(project);

            mockProjectRepository.Setup(r => r.GetAll()).Returns(list);
            mockProjectRepository.Setup(r => r.Delete(project.Id));
            mockProjectRepository.Setup(r => r.GetById(project.Id)).Returns(project);

            projectLogic.Delete(project.Id);

            List<Project> projectSaved = projectLogic.GetAll();

            mockProjectRepository.VerifyAll();

            Assert.IsTrue(projectSaved.SequenceEqual(list));
        }

        [TestMethod]
        public void ProjectUpdateOk()
        {
            Project updatedProject = new Project("Project Lab");

            List<Project> projects = new List<Project>();
            projects.Add(updatedProject);

            mockProjectRepository.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(updatedProject);
            mockProjectRepository.Setup(r => r.GetAll()).Returns(projects);
            mockProjectRepository.Setup(x => x.Update(project.Id, updatedProject)).Returns(updatedProject);

            Project projectUpdate = projectLogic.Update(project.Id, updatedProject);

            mockProjectRepository.VerifyAll();

            Assert.AreEqual(projectUpdate.Name, updatedProject.Name);
        }

        [TestMethod]
        public void DeleteUserOk()
        {
            project.Users.Add(tester);
            Project projectNoTesters = new Project("Proyecto sin usuarios");

            userMock.Setup(m => m.Get(tester.Id)).Returns(tester);
            mockProjectRepository.Setup(x => x.GetById(project.Id)).Returns(project);
            mockProjectRepository.Setup(x => x.Update(It.IsAny<Guid>(), project)).Returns(projectNoTesters);
            mockProjectRepository.Setup(x => x.GetAll()).Returns(new List<Project> { project });
            projectLogic.DeleteUser(project.Id, ref tester);

            List<User> list = new List<User>();

            var ret = projectLogic.GetAllTesters(project);
            mockProjectRepository.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void AssignUserOk()
        {
            mockProjectRepository.Setup(x => x.GetById(project.Id)).Returns(project);
            mockProjectRepository.Setup(x => x.Update(It.IsAny<Guid>(), project)).Returns(project);

            projectLogic.AssignUser(project.Id, ref tester);

            List<User> users = new List<User>();
            users.Add(tester);

            var returnedUsers = projectLogic.GetAllTesters(project);
            mockProjectRepository.VerifyAll();

            Assert.IsTrue(returnedUsers.SequenceEqual(users));
        }

        [TestMethod]
        public void GetAllTesterInProject()
        {
            project.Users.Add(tester);

            List<User> users = new List<User>();
            users.Add(tester);

            mockProjectRepository.Setup(x => x.GetById(project.Id)).Returns(project);

            List<User> allUsers = projectLogic.GetAllTesters(project);

            mockProjectRepository.VerifyAll();

            Assert.IsTrue(allUsers.SequenceEqual(users));
        }

        [TestMethod]
        public void GetAllDeveloperInProject()
        {
            project.Users.Add(developer);

            List<User> users = new List<User>();
            users.Add(developer);

            mockProjectRepository.Setup(x => x.GetById(project.Id)).Returns(project);
            List<User> usersGeted = projectLogic.GetAllDevelopers(project);
            mockProjectRepository.VerifyAll();

            Assert.IsTrue(usersGeted.SequenceEqual(users));
        }

        [TestMethod]
        public void CalculateTotalDuration()
        {
            Bug oneBug = new Bug(project, 1234, "Error de login",
                "Intento inicio de sesion", "2.0", new State(State.active), 10);
            Task oneTask = new Task(project, "One Task", 2000, 15);

            project.Bugs.Add(oneBug);
            project.Tasks.Add(oneTask);

            mockProjectRepository.Setup(x => x.GetById(project.Id)).Returns(project);

            project = projectLogic.Get(project.Id);

            int projectDuration = oneBug.Duration + oneTask.Duration;

            mockProjectRepository.VerifyAll();
            Assert.IsTrue(projectDuration == project.Duration);
        }

        [TestMethod]
        public void CalculateTotalPrice()
        {
            Bug oneBug = new Bug(project, 1234, "Error de login",
                "Intento inicio de sesion", "2.0", new State(State.done), 10);
            Task oneTask = new Task(project, "One Task", 2000, 15);

            oneBug.SolvedBy = developer;
            project.Bugs.Add(oneBug);
            project.Tasks.Add(oneTask);

            mockProjectRepository.Setup(x => x.GetById(project.Id)).Returns(project);

            project = projectLogic.Get(project.Id);

            int projectPrice = (oneBug.Duration * oneBug.SolvedBy.Price) + (oneTask.Price * oneTask.Duration);

            mockProjectRepository.VerifyAll();
            Assert.IsTrue(projectPrice == project.Price);
        }
    }
}