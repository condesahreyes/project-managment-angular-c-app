using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using BusinessLogic.UserRol;
using DataAccessInterface;
using System.Linq;
using System;
using Domain;
using Moq;
using BusinessLogic;

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
            tester = new User("Fiorella", "Petrone", "fioPetro", "fio1245", "fiore@gmail.com", rolTester);
            developer = new User("Juan", "Gomez", "juanG", "juann245", "juan@gmail.com", rolDeveloper);
        }

        [TestMethod]
        public void CreateProjectOk()
        {
            mock.Setup(x => x.Create(project)).Returns(project);
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

        [ExpectedException(typeof(Exception), "The project doesn't exists")]
        [TestMethod]
        public void GetProjectByIdFail()
        {
            Guid id = Guid.NewGuid();
            Project projectoEmpty = null;
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(projectoEmpty);
            var ret = projectLogic.Get(id);
        }

        [TestMethod]
        public void RemoveProjectOk()
        {
            List<Project> list = new List<Project>();

            mock.Setup(r => r.GetAll()).Returns(list);
            mock.Setup(r => r.Delete(project.Id));

            projectLogic.Delete(project.Id);

            List<Project> projectSaved = projectLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(projectSaved.SequenceEqual(list));
        }

        [TestMethod]
        public void ProjectUpdateOk()
        {
            var updatedProject = new Project("Project Lab");

            mock.Setup(x => x.Update(project.Id, updatedProject)).Returns(updatedProject);
            var ret = projectLogic.Update(project.Id, updatedProject);
            mock.VerifyAll();
            Assert.AreEqual(ret.Name, updatedProject.Name);
        }

        [TestMethod]
        public void DeleteTesterOk()
        {
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var proj = projectLogic.Get(project.Id);

            projectMock.Setup(x => x.AssignTester(proj, tester));
            projectLogic.AssignTester(proj, tester);

            projectMock.Setup(m => m.DeleteTester(proj, tester));
            projectLogic.DeleteTester(proj, tester);

            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllTesters(proj)).Returns(list);
            var ret = projectLogic.GetAllTesters(proj);
            mock.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void DeleteDeveloperOk()
        {
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var proj = projectLogic.Get(project.Id);

            projectMock.Setup(x => x.AssignDeveloper(proj, developer));
            projectLogic.AssignDeveloper(proj, developer);

            projectMock.Setup(m => m.DeleteDeveloper(proj, developer));
            projectLogic.DeleteDeveloper(proj, developer);

            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllDevelopers(proj)).Returns(list);
            var ret = projectLogic.GetAllDevelopers(proj);
            mock.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void AssignDeveloperOk()
        {
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var ret = projectLogic.Get(project.Id);

            projectMock.Setup(x => x.AssignDeveloper(ret, developer));
            projectLogic.AssignDeveloper(ret, developer);

            List<User> list = new List<User>();
            list.Add(developer);

            projectMock.Setup(x => x.GetAllDevelopers(ret)).Returns(list);
            var retList = projectLogic.GetAllDevelopers(ret);
            mock.VerifyAll();

            Assert.IsTrue(retList.SequenceEqual(list));
        }

        [TestMethod]
        public void AssignTesterOk()
        {
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var ret = projectLogic.Get(project.Id);

            projectMock.Setup(x => x.AssignTester(ret, tester));
            projectLogic.AssignTester(ret, tester);

            List<User> list = new List<User>();
            list.Add(tester);

            projectMock.Setup(x => x.GetAllTesters(ret)).Returns(list);
            var retList = projectLogic.GetAllTesters(ret);
            mock.VerifyAll();

            Assert.IsTrue(retList.SequenceEqual(list));
        }

        [TestMethod]
        public void GetAllTesterOk()
        {
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var proj = projectLogic.Get(project.Id);

            projectMock.Setup(x => x.AssignTester(proj, tester));
            projectLogic.AssignTester(proj, tester);

            List<User> list = new List<User>();
            list.Add(tester);

            projectMock.Setup(x => x.GetAllTesters(proj)).Returns(list);
            List<User> ret = projectLogic.GetAllTesters(proj);
            mock.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void GetAllDeveloperOk()
        {
            mock.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(project);
            var proj = projectLogic.Get(project.Id);

            projectMock.Setup(x => x.AssignDeveloper(proj, developer));
            projectLogic.AssignDeveloper(proj, developer);

            List<User> list = new List<User>();
            list.Add(developer);

            projectMock.Setup(x => x.GetAllDevelopers(proj)).Returns(list);
            List<User> ret = projectLogic.GetAllDevelopers(proj);
            mock.VerifyAll();

            Assert.IsTrue(ret.SequenceEqual(list));
        }

    }
}