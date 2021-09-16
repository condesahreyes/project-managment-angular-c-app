using BusinessLogic;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicTest
{
    [TestClass]
    public class BugLogicTest
    {
        private Mock<IRepository<Bug,int>> mock;
 
        private static string version = "3.0";
        private static string stateActive = "Activo";
        private static string stateResolved = "Resuelto";
        private static string name = "Error de login";
        private static string name2 = "Error de UI";
        private static Project project = new Project("Montes Del Plata");
        private static string domain = "Al intentar iniciar sesión, no reconoce usuario";

        private static int id = 1234;

        [TestInitialize]
        public void Setup()
        {
            mock = new Mock<IRepository<Bug>>(MockBehavior.Strict);
        }

        [TestMethod]
        public void CreateBugActive()
        {
            var bug = new Bug(project, id, name, domain, version, stateActive);

            mock.Setup(x => x.Create(bug)).Returns(bug);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugSaved = bugLogic.Create(bug);

            mock.VerifyAll();

            Assert.AreEqual(bug, bugSaved);
        }

        [TestMethod]
        public void CreateBugResolved()
        {
            var bug = new Bug(project, id, name, domain, version, stateResolved);

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
            var bugs = new List<Bug>
            {
                new Bug(project, id, name, domain, version, stateActive),
                new Bug(project, id, name2, domain, version, stateActive),
            };

            mock.Setup(r => r.GetAll()).Returns(bugs);
            var bugLogic = new BugLogic(mock.Object);

            IEnumerable<Bug> bugsSaved= bugLogic.GetAll();

            mock.VerifyAll();
            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void DeleteBug()
        {
            var bugs = new List<Bug>();
            var bug = new Bug(project, id, name, domain, version, "desactivado");

            mock.Setup(r => r.GetAll()).Returns(bugs);
            mock.Setup(r => r.Delete(bug));

            var bugLogic = new BugLogic(mock.Object);

            bugLogic.Delete(bug);

            IEnumerable<Bug> bugSaved = bugLogic.GetAll();
            
            mock.VerifyAll();

            Assert.IsTrue(bugSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void UpdateBugProject()
        {
            Project newProject = new Project("Nuevo proyecto");
            var bug = new Bug(project, id, name, domain, version, stateActive);
            var bugUpdate = new Bug(newProject, id, name, domain, version, stateActive);
           
            List<Bug> bugs = new List<Bug>();
            bugs.Add(bugUpdate);

            mock.Setup(r => r.GetAll()).Returns(bugs);
            mock.Setup(r => r.Update(bug, bugUpdate));

            var bugLogic = new BugLogic(mock.Object);

            bugLogic.Update(bug, bugUpdate);

            IEnumerable<Bug> bugSaved = bugLogic.GetAll();

            mock.VerifyAll();
            string nombre = bugSaved.First().Project.Name;
            Assert.IsTrue(bugSaved.First().Project.Name=="Nuevo proyecto");
        }

        [TestMethod]
        public void UpdateBugName()
        {
            var bug = new Bug(project, id, name, domain, version, stateActive);
            var bugUpdate = new Bug(project, id, name2, domain, version, stateActive);

            List<Bug> bugs = new List<Bug>();
            bugs.Add(bugUpdate);

            mock.Setup(r => r.GetAll()).Returns(bugs);
            mock.Setup(r => r.Update(bug, bugUpdate));

            var bugLogic = new BugLogic(mock.Object);

            bugLogic.Update(bug, bugUpdate);

            IEnumerable<Bug> bugSaved = bugLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(bugSaved.First().Name == name2);
        }

        [TestMethod]
        public void UpdateBugDomain()
        {
            var bug = new Bug(project, id, name, domain, version, stateActive);
            var bugUpdate = new Bug(project, id, name2, "Otro dominio", version, stateActive);

            List<Bug> bugs = new List<Bug>();
            bugs.Add(bugUpdate);

            mock.Setup(r => r.GetAll()).Returns(bugs);
            mock.Setup(r => r.Update(bug, bugUpdate));

            var bugLogic = new BugLogic(mock.Object);

            bugLogic.Update(bug, bugUpdate);

            IEnumerable<Bug> bugsSaved = bugLogic.GetAll();

            mock.VerifyAll();
            Assert.IsTrue(bugsSaved.First().Domain == "Otro dominio");
        }

        [TestMethod]
        public void UpdateBugVersion()
        {
            var bug = new Bug(project, id, name, domain, version, stateActive);
            var bugUpdate = new Bug(project, id, name2, domain, "3.5", stateActive);

            List<Bug> bugs = new List<Bug>();
            bugs.Add(bugUpdate);

            mock.Setup(r => r.GetAll()).Returns(bugs);
            mock.Setup(r => r.Update(bug, bugUpdate));

            var bugLogic = new BugLogic(mock.Object);

            bugLogic.Update(bug, bugUpdate);

            IEnumerable<Bug> bugSaved = bugLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(bugSaved.First().Version == "3.5");
        }

        [TestMethod]
        public void UpdateBugState()
        {
            var bug = new Bug(project, id, name, domain, version, stateActive);
            var bugUpdate = new Bug(project, id, name2, domain, "3.5", stateResolved);

            List<Bug> bugs = new List<Bug>();
            bugs.Add(bugUpdate);

            mock.Setup(r => r.GetAll()).Returns(bugs);
            mock.Setup(r => r.Update(bug, bugUpdate));

            var bugLogic = new BugLogic(mock.Object);

            bugLogic.Update(bug, bugUpdate);

            IEnumerable<Bug> bugSaved = bugLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(bugSaved.First().State == stateResolved);
        }

        [TestMethod]
        public void GetBug()
        {
            var bug = new Bug(project, id, name, domain, version, stateActive);

            mock.Setup(r => r.Get(bug.Id)).Returns(bug);

            var bugLogic = new BugLogic(mock.Object);

            Bug bugGet = bugLogic.Get(bug.Id);

            mock.VerifyAll();
            Assert.AreEqual(bugGet, bug);
        }

    }
}
