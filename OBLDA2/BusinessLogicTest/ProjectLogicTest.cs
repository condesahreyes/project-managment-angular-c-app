using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic;
using BusinessLogic.UserRol;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class ProjectLogicTest
    {

        private Mock<IRepository<Project, Guid>> mock;
        private Mock<IProjectLogic> projectMock;


        private ProjectLogic projectLogic;
        private Project project;
        private User tester;
        private Rol rolTester;
        private User developer;
        private Rol rolDeveloper;

        [TestInitialize]
        public void Setup()
        {
            mock = new Mock<IRepository<Project,Guid>>(MockBehavior.Strict);
            projectMock = new Mock<IProjectLogic>(MockBehavior.Strict);

            this.projectLogic = new ProjectLogic(mock.Object, projectMock.Object);

            Guid id = new Guid();
            project = new Project(id, "Project - Pineapple ");
            rolTester = new Rol(id, "Tester");
            tester = new User(id, "Fiorella", "Petrone", "fioPetro", "fio1245", "fiore@gmail.com", rolTester);
            developer = new User(id, "Juan", "Gomez", "juanG", "juann245", "juan@gmail.com", rolDeveloper);

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
            mock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(project);
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
            mock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(projectoEmpty);
            var ret = projectLogic.Get(id);

        }

        [TestMethod]
        public void RemoveProjectOk()
        {
            mock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(project);
            mock.Setup(m => m.Delete(project.Id));
           
            projectLogic.Delete(project.Id);

            Project projectNull = null;
            mock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(projectNull);

            List<Project> list = new List<Project>();
            mock.Setup(x => x.GetAll()).Returns(list);

            var ret = projectLogic.GetAll();
            mock.VerifyAll();

            Assert.IsTrue(ret.Count == 0);

        }

        [TestMethod]
        public void ProjectUpdateOk()
        {

            Guid id = Guid.NewGuid();
            var updatedProject = new Project(id, "Project Lab");

            mock.Setup(x => x.Update(project.Id, updatedProject)).Returns(updatedProject);
            var ret = projectLogic.Update(project.Id, updatedProject);
            mock.VerifyAll();
            Assert.AreEqual(ret.Name, updatedProject.Name);

        }

        [TestMethod]
        public void DeleteTesterOk()
        {
            projectMock.Setup(m => m.DeleteTester(project, tester.Id));
            projectLogic.DeleteTester(project, tester.Id);

            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllTesters(project)).Returns(list);
            var ret = projectLogic.GetAllTesters(project);
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);

        }

        [TestMethod]
        public void DeleteDeveloperOk()
        {
            projectMock.Setup(m => m.DeleteDeveloper(project, developer.Id));
            projectLogic.DeleteDeveloper(project, developer.Id);

            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllDevelopers(project)).Returns(list);
            var ret = projectLogic.GetAllDevelopers(project);
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);

        }

        [TestMethod]
        public void AssignDeveloperOk()
        {
            projectMock.Setup(x => x.AssignDeveloper(project, developer.Id));
            projectLogic.AssignDeveloper(project, developer.Id);

            List<User> list = new List<User>();
            list.Add(developer);

            projectMock.Setup(x => x.GetAllDevelopers(project)).Returns(list);
            var ret = projectLogic.GetAllDevelopers(project);
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);


        }

        [TestMethod]
        public void AssignTesterOk()
        {
            projectMock.Setup(x => x.AssignTester(project, tester.Id));
            projectLogic.AssignTester(project, tester.Id);

            List<User> list = new List<User>();
            list.Add(tester);

            projectMock.Setup(x => x.GetAllTesters(project)).Returns(list);
            var ret = projectLogic.GetAllTesters(project);
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);

        }

        [TestMethod]
        public void ImportBugsByProviderOk()
        {
            // implementar cuando este terminado


        }

        [TestMethod]
        public void GetAllTesterOk()
        {
            List<User> list = new List<User>();
            list.Add(tester);
            projectMock.Setup(x => x.GetAllTesters(project)).Returns(list);
            List<User> ret = projectLogic.GetAllTesters(project);
            mock.VerifyAll();
            Assert.IsTrue(ret.SequenceEqual(list));

        }

        [TestMethod]
        public void GetAllDeveloperOk()
        {
            List<User> list = new List<User>();
            list.Add(developer);
            projectMock.Setup(x => x.GetAllDevelopers(project)).Returns(list);
            List<User> ret = projectLogic.GetAllDevelopers(project);
            mock.VerifyAll();
            Assert.IsTrue(ret.SequenceEqual(list));


        }

        [TestMethod]
        public void GetAllFixedBugsByDeveloperOk()
        {
            int fixedBugs = 3;
            projectMock.Setup(x => x.GetAllFixedBugsByDeveloper(developer.Id)).Returns(fixedBugs);

            var ret = projectLogic.GetAllFixedBugsByDeveloper(developer.Id);
            projectMock.VerifyAll();

            Assert.AreEqual(fixedBugs, ret);

        }

    }
}
