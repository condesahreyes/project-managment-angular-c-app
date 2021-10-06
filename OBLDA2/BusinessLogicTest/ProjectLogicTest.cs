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

namespace BusinessLogicTest
{
    [TestClass]
    public class ProjectLogicTest
    {

        private Mock<IProjectRepository> mock;
        private Mock<IUserLogic> userMock;
        private Mock<IProjectLogic> projectMock;
        private Mock<IBugLogic> bugMock;

        private ProjectLogic projectLogic;
        private Project project;

        private User developer;
        private User tester;

        private Rol rolDeveloper;
        private Rol rolTester;

        [TestInitialize]
        public void Setup()
        {
            mock = new Mock<IProjectRepository>(MockBehavior.Strict);
            projectMock = new Mock<IProjectLogic>(MockBehavior.Strict);
            userMock = new Mock<IUserLogic>(MockBehavior.Strict);
            bugMock = new Mock<IBugLogic>(MockBehavior.Strict);

            this.projectLogic = new ProjectLogic(mock.Object, userMock.Object);

            project = new Project("Project - Pineapple ");

            rolTester = new Rol(Rol.tester);
            rolDeveloper = new Rol(Rol.developer);

            tester = new User("Fiorella", "Petrone", "fioPetro", "fio1245",
                "fiore@gmail.com", rolTester);
            developer = new User("Juan", "Gomez", "juanG", "juann245", 
                "juan@gmail.com", rolDeveloper);
        }

        [TestMethod]
        public void CreateProjectOk()
        {
            List<Project> list = new List<Project>();
            list.Add(project);
            mock.Setup(x => x.Create(project)).Returns(project);
            mock.Setup(x => x.GetAll()).Returns(list);
            var projectCreated = projectLogic.Create(project);
            mock.VerifyAll();
            Assert.AreEqual(project, projectCreated);
        }
        
        [TestMethod]
        public void GetAllProjects()
        {
            List<Project> list = new List<Project>();
            list.Add(project);
            mock.Setup(x => x.GetAll()).Returns(list);
            List<Project> ret = projectLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void GetProjectByIdOk()
        {
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var ret = projectLogic.Get(project.Id);

            mock.VerifyAll();

            Assert.IsTrue(ret.Equals(project));
        }

        [ExpectedException(typeof(NoObjectException))]
        [TestMethod]
        public void GetProjectByIdFail()
        {
            Guid id = Guid.NewGuid();
            Project projectoEmpty = null;
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(projectoEmpty);
            Project project = projectLogic.Get(id);
        }

        [TestMethod]
        public void RemoveProjectOk()
        {
            List<Project> list = new List<Project>();
            list.Add(project);

            mock.Setup(r => r.GetAll()).Returns(list);
            mock.Setup(r => r.Delete(project.Id));
            mock.Setup(r => r.GetById(project.Id)).Returns(project);

            projectLogic.Delete(project.Id);

            List<Project> projectSaved = projectLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(projectSaved.SequenceEqual(list));
        }

        [TestMethod]
        public void ProjectUpdateOk()
        {
            Project updatedProject = new Project("Project Lab");

            List<Project> projects = new List<Project>();
            projects.Add(updatedProject);

            mock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(updatedProject);
            mock.Setup(r => r.GetAll()).Returns(projects);
            mock.Setup(x => x.Update(project.Id, updatedProject)).Returns(updatedProject);

            Project projectUpdate = projectLogic.Update(project.Id, updatedProject);

            mock.VerifyAll();

            Assert.AreEqual(projectUpdate.Name, updatedProject.Name);
        }

        [TestMethod]
        public void DeleteUserOk()
        {
            project.Users.Add(tester);
            Project projectNoTesters = new Project("Proyecto sin usuarios");

            userMock.Setup(m => m.Get(tester.Id)).Returns(tester);
            mock.Setup(x => x.GetById(project.Id)).Returns(project);
            mock.Setup(x => x.Update(It.IsAny<Guid>(), project)).Returns(projectNoTesters);
            projectLogic.DeleteUser(project.Id, ref tester);

            List<User> list = new List<User>();

            var ret = projectLogic.GetAllTesters(project);
            mock.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void AssignUserOk()
        {
            mock.Setup(x => x.GetById(project.Id)).Returns(project);
            mock.Setup(x => x.Update(It.IsAny<Guid>(), project)).Returns(project);

            projectLogic.AssignUser(project.Id, ref tester);

            List<User> list = new List<User>();
            list.Add(tester);

            var retList = projectLogic.GetAllTesters(project);
            mock.VerifyAll();

            Assert.IsTrue(retList.SequenceEqual(list));
        }

        [TestMethod]
        public void GetAllTesterOk()
        {
            project.Users.Add(tester);

            List<User> users = new List<User>();
            users.Add(tester);

            mock.Setup(x => x.GetById(project.Id)).Returns(project);
            List<User> usersGeted = projectLogic.GetAllTesters(project);
            mock.VerifyAll();

            Assert.IsTrue(usersGeted.SequenceEqual(users));
        }

        [TestMethod]
        public void GetAllDeveloperOk()
        {
            project.Users.Add(developer);

            List<User> users = new List<User>();
            users.Add(developer);

            mock.Setup(x => x.GetById(project.Id)).Returns(project);
            List<User> usersGeted = projectLogic.GetAllDevelopers(project);
            mock.VerifyAll();

            Assert.IsTrue(usersGeted.SequenceEqual(users));
        }

    }
}