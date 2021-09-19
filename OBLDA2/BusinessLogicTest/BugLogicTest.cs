using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using DataAccessInterface;
using BusinessLogic;
using System.Linq;
using Domain;
using System;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class BugLogicTest
    {
        private Mock<IRepository<Bug, int>> mock;

        private static Project project = new Project(new Guid(), "Montes Del Plata");

        private static string domain = "Al intentar iniciar sesión, no reconoce usuario";
        private static string stateResolved = "Resuelto";
        private static string otherName = "Error de UI";
        private static string name = "Error de login";
        private static string stateActive = "Activo";
        private static string version = "3.0";
        
        private static int id = 1234;

        Bug bug;
        Bug otherBug;

        [TestInitialize]
        public void Setup()
        {
            mock = new Mock<IRepository<Bug, int>>(MockBehavior.Strict);
            InicializarBugs();
        }

        private void InicializarBugs()
        {
            Project project = new Project(new Guid(), "Montes Del Plata");

            bug = new Bug(project, id, name, domain, version, stateActive);
            otherBug = new Bug(project, id, otherName, domain, version, stateActive);
        }

        [TestMethod]
        public void CreateBugActive()
        {
            mock.Setup(x => x.Create(bug)).Returns(bug);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bug);

            mock.VerifyAll();

            Assert.AreEqual(bug, bugSaved);
        }

        [TestMethod]
        public void CreateBugResolved()
        {
            mock.Setup(x => x.Create(bug)).Returns(bug);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bug);

            mock.VerifyAll();

            Assert.AreEqual(bug, bugSaved);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBugInvalidId()
        {
            var bugInvalidProject = new Bug(project, 12345,
                name, domain, version, stateActive);

            mock.Setup(x => x.Create(bugInvalidProject)).Returns(bugInvalidProject);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidProject);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBugInvalidName()
        {
            var bugInvalidName = new Bug(project, id, "Nombre mayor a 60 caracteres " +
                "Nombre mayor a 60 caracteres mal", domain, version, stateActive);

            mock.Setup(x => x.Create(bugInvalidName)).Returns(bugInvalidName);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBugInvalidDomain()
        {
            var bugInvalidDomain = new Bug(project, id, name, "Dominio mayor a 150 caracteres " +
                "Dominio mayor a 150 caracteres Dominio mayor a 150 caracteres Dominio mayor " +
                "a 150 caracteres Dominio mayor a 150 caracteres 123456789", version, stateActive);

            mock.Setup(x => x.Create(bugInvalidDomain)).Returns(bugInvalidDomain);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidDomain);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBugInvalidVersion()
        {
            var bugInvalidVersion = new Bug(project, id, name, domain, "mayor a 10 ", stateActive);

            mock.Setup(x => x.Create(bugInvalidVersion)).Returns(bugInvalidVersion);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidVersion);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBugInvalidState()
        {
            var bugInvalidState = new Bug(project, id, name, domain, version, "desactivado");

            mock.Setup(x => x.Create(bugInvalidState)).Returns(bugInvalidState);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidState);
        }

        [TestMethod]
        public void GetAllBugs()
        {
            List<Bug> bugs = new List<Bug> { bug, otherBug };

            mock.Setup(r => r.GetAll()).Returns(bugs);
            var bugLogic = new BugLogic(mock.Object);

            List<Bug> bugsSaved = bugLogic.GetAll();

            mock.VerifyAll();
            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void DeleteBug()
        {
            var bugs = new List<Bug>();

            mock.Setup(r => r.GetAll()).Returns(bugs);
            mock.Setup(r => r.Delete(bug.Id));

            var bugLogic = new BugLogic(mock.Object);

            bugLogic.Delete(bug.Id);

            List<Bug> bugSaved = bugLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(bugSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void UpdateBugProject()
        {
            Project newProject = new Project(new Guid(), "Nuevo proyecto");
            var bugUpdate = new Bug(newProject, id, name, domain, version, stateActive);

            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);

            var bugLogic = new BugLogic(mock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate);

            mock.VerifyAll();
            string nombre = bugNew.Project.Name;
            Assert.IsTrue(bugNew.Project.Name == "Nuevo proyecto");
        }

        [TestMethod]
        public void UpdateBugName()
        {
            var bugUpdate = new Bug(project, id, otherName, domain, version, stateActive);

            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);

            var bugLogic = new BugLogic(mock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate);

            mock.VerifyAll();

            Assert.IsTrue(bugNew.Name == otherName);
        }

        [TestMethod]
        public void UpdateBugDomain()
        {
            var bugUpdate = new Bug(project, id, otherName, "Otro dominio", version, stateActive);
            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);

            var bugLogic = new BugLogic(mock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate);

            mock.VerifyAll();

            Assert.IsTrue(bugNew.Domain == "Otro dominio");
        }

        [TestMethod]
        public void UpdateBugVersion()
        {
            var bugUpdate = new Bug(project, id, otherName, domain, "3.5", stateActive);

            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);

            var bugLogic = new BugLogic(mock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate);

            mock.VerifyAll();

            Assert.IsTrue(bugNew.Version == "3.5");
        }

        [TestMethod]
        public void UpdateBugState()
        {
            var bugUpdate = new Bug(project, id, otherName, domain, "3.5", stateResolved);

            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);

            var bugLogic = new BugLogic(mock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate);

            mock.VerifyAll();
            Assert.IsTrue(bugNew.State == stateResolved);
        }

        [TestMethod]
        public void GetBug()
        {
            mock.Setup(r => r.Get(bug.Id)).Returns(bug);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugGet = bugLogic.Get(bug.Id);

            mock.VerifyAll();
            Assert.AreEqual(bugGet, bug);
        }

    }
}
