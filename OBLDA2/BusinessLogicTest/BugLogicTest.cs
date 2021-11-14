using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using DataAccessInterface;
using BusinessLogic;
using System.Linq;
using Exceptions;
using Domain;
using System;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class BugLogicTest
    {
        private Mock<IBugRepository> mock;
        private Mock<IRepository<State, Guid>> stateMock;
        private Mock<IProjectLogic> projectMock;

        private static Project project = new Project("Montes Del Plata");
        private static State stateActive = new State(State.active);
        private static State stateResolved = new State(State.done);

        private static string domain = "Al intentar iniciar sesión, no reconoce usuario";
        private static string otherName = "Error de UI";
        private static string name = "Error de login";
        private static string version = "3.0";
        
        private static int id = 1234;
        private static int duration = 0;

        Bug bug;
        Bug otherBug;

        [TestInitialize]
        public void Setup()
        {
            mock = new Mock<IBugRepository>(MockBehavior.Strict);
            stateMock = new Mock<IRepository<State, Guid>>(MockBehavior.Strict);
            projectMock = new Mock<IProjectLogic>(MockBehavior.Strict);

            InicializarBugs();
            ConfigurationStateMock();
        }

        private void InicializarBugs()
        {
            Project project = new Project("Montes Del Plata");
            State state = new State(State.active);

            bug = new Bug(project, id, name, domain, version, state, duration);
            otherBug = new Bug(project, id, otherName, domain, version, state, duration);
        }

        private void ConfigurationStateMock()
        {
            List<State> states = new List<State>
            {
                stateActive, stateResolved,
            };

            stateMock.Setup(x => x.GetAllGeneric()).Returns(states);
        }

        [TestMethod]
        public void CreateBugActive()
        {
            Bug bugNull = null;

            mock.Setup(x => x.Create(bug)).Returns(bug);
            mock.Setup(x => x.GetById(bug.Id)).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.Create(bug);

            mock.VerifyAll();

            Assert.AreEqual(bug, bugSaved);
        }

        [TestMethod]
        public void CreateBugResolved()
        {
            Bug bugNull = null;

            mock.Setup(x => x.Create(bug)).Returns(bug);
            mock.Setup(x => x.GetById(bug.Id)).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.Create(bug);

            mock.VerifyAll();

            Assert.AreEqual(bug, bugSaved);
        }

        [TestMethod]
        public void CreateBugByUser()
        {
            Bug bugNull = null;

            mock.Setup(x => x.Create(bug)).Returns(bug);
            mock.Setup(x => x.GetById(bug.Id)).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);
            projectMock.Setup(x => x.IsUserAssignInProject(project.Name, It.IsAny<Guid>()));

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.CreateByUser(bug, Guid.NewGuid());

            mock.VerifyAll();

            Assert.AreEqual(bug, bugSaved);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateBugInvalidId()
        {
            var bugInvalidProject = new Bug(project, 12345,
                name, domain, version, stateActive, 0);

            Bug bugNull = null;

            mock.Setup(x => x.Create(It.IsAny<Bug>())).Returns(bug);
            mock.Setup(x => x.GetById(It.IsAny<int>())).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidProject);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateBugInvalidName()
        {
            State stateActive = new State(State.active);
            var bugInvalidName = new Bug(project, id, "Nombre mayor a 60 caracteres " +
                "Nombre mayor a 60 caracteres mal", domain, version, stateActive, 0);

            Bug bugNull = null;

            mock.Setup(x => x.Create(bug)).Returns(bug);
            mock.Setup(x => x.GetById(bug.Id)).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidName);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateBugInvalidDomain()
        {
            var bugInvalidDomain = new Bug(project, id, name, "Dominio mayor a 150 caracteres " +
                "Dominio mayor a 150 caracteres Dominio mayor a 150 caracteres Dominio mayor " +
                "a 150 caracteres Dominio mayor a 150 caracteres 123456789", version, stateActive, duration);

            Bug bugNull = null;

            mock.Setup(x => x.Create(bug)).Returns(bug);
            mock.Setup(x => x.GetById(bug.Id)).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidDomain);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateBugInvalidVersion()
        {
            var bugInvalidVersion = new Bug(project, id, name, domain, "mayor a 10 ", stateActive, duration);
            Bug bugNull = null;

            mock.Setup(x => x.Create(bug)).Returns(bug);
            mock.Setup(x => x.GetById(bug.Id)).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidVersion);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDataObjException))]
        public void CreateBugInvalidState()
        {
            State invalidState = new State("desactivado");
            var bugInvalidState = new Bug(project, id, name, domain, version, invalidState, duration);
            Bug bugNull = null;

            mock.Setup(x => x.Create(bug)).Returns(bug);
            mock.Setup(x => x.GetById(bug.Id)).Returns(bugNull);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugSaved = bugLogic.Create(bugInvalidState);
        }

        [TestMethod]
        public void GetAllBugs()
        {
            List<Bug> bugs = new List<Bug> { bug, otherBug };

            mock.Setup(r => r.GetAll()).Returns(bugs);
            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

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
            mock.Setup(x => x.GetById(bug.Id)).Returns(bug);
            projectMock.Setup(p => p.IsUserAssignInProject(It.IsAny<string>(), It.IsAny<Guid>()));

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            bugLogic.Delete(bug.Id, It.IsAny<Guid>());

            List<Bug> bugSaved = bugLogic.GetAll();

            mock.VerifyAll();

            Assert.IsTrue(bugSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void UpdateBugProject()
        {
            Project newProject = new Project("Nuevo proyecto");
            Bug oneBugUpdate = new Bug(newProject, id, name, domain, version, stateActive, duration);

            projectMock.Setup(p => p.IsUserAssignInProject(It.IsAny<string>(), It.IsAny<Guid>()));
            mock.Setup(x => x.GetById(bug.Id)).Returns(bug);
            mock.Setup(r => r.Update(bug.Id, oneBugUpdate)).Returns(oneBugUpdate);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(newProject);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);
            var bugNew = bugLogic.Update(bug.Id, oneBugUpdate, It.IsAny<Guid>());

            mock.VerifyAll();
            string nombre = bugNew.Project.Name;
            Assert.IsTrue(bugNew.Project.Name == "Nuevo proyecto");
        }

        [TestMethod]
        public void UpdateBugName()
        {
            projectMock.Setup(p => p.IsUserAssignInProject(It.IsAny<string>(), It.IsAny<Guid>()));
            var bugUpdate = new Bug(project, id, otherName, domain, version, stateActive, duration);

            mock.Setup(x => x.GetById(bug.Id)).Returns(bug);
            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate, It.IsAny<Guid>());

            mock.VerifyAll();

            Assert.IsTrue(bugNew.Name == otherName);
        }

        [TestMethod]
        public void UpdateBugDomain()
        {
            projectMock.Setup(p => p.IsUserAssignInProject(It.IsAny<string>(), It.IsAny<Guid>()));
            var bugUpdate = new Bug(project, id, otherName, "Otro dominio", version, stateActive, duration);
            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);

            mock.Setup(x => x.GetById(bug.Id)).Returns(bug);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);
            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate, It.IsAny<Guid>());

            mock.VerifyAll();

            Assert.IsTrue(bugNew.Domain == "Otro dominio");
        }

        [TestMethod]
        public void UpdateBugVersion()
        {
            var bugUpdate = new Bug(project, id, otherName, domain, "3.5", stateActive, duration);

            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);
            projectMock.Setup(p => p.IsUserAssignInProject(It.IsAny<string>(), It.IsAny<Guid>()));
            mock.Setup(x => x.GetById(bug.Id)).Returns(bug);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);
            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate, It.IsAny<Guid>());

            mock.VerifyAll();

            Assert.IsTrue(bugNew.Version == "3.5");
        }

        [TestMethod]
        public void UpdateBugState()
        {
            var bugUpdate = new Bug(project, id, otherName, domain, "3.5", stateResolved, duration);

            mock.Setup(r => r.Update(bug.Id, bugUpdate)).Returns(bugUpdate);
            projectMock.Setup(p => p.IsUserAssignInProject(It.IsAny<string>(), It.IsAny<Guid>()));
            mock.Setup(x => x.GetById(bug.Id)).Returns(bug);
            projectMock.Setup(x => x.ExistProjectWithName(It.IsAny<Project>())).Returns(project);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);
            var bugNew = bugLogic.Update(bug.Id, bugUpdate, It.IsAny<Guid>());

            mock.VerifyAll();
            Assert.IsTrue(bugNew.State == stateResolved);
        }

        [TestMethod]
        public void GetBug()
        {
            projectMock.Setup(p => p.IsUserAssignInProject(It.IsAny<string>(), It.IsAny<Guid>()));
            mock.Setup(r => r.GetById(bug.Id)).Returns(bug);

            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            Bug bugGet = bugLogic.Get(bug.Id, It.IsAny<Guid>());

            mock.VerifyAll();
            Assert.AreEqual(bugGet, bug);
        }

        [TestMethod]
        public void GetBugsByName()
        {
            bug.Name = otherBug.Name;
            List<Bug> bugs = new List<Bug> { bug, otherBug };

            mock.Setup(r => r.GetAll()).Returns(bugs);
            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            List<Bug> bugsSaved = bugLogic.GetBugsByName(bug.Name);

            mock.VerifyAll();
            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void GetBugsByState()
        {
            List<Bug> bugs = new List<Bug> { bug, otherBug };

            mock.Setup(r => r.GetAll()).Returns(bugs);
            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            List<Bug> bugsSaved = bugLogic.GetBugsByState(bug.State.Name);

            mock.VerifyAll();
            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }

        [TestMethod]
        public void GetBugsByProject()
        {
            List<Bug> bugs = new List<Bug> { bug, otherBug };

            mock.Setup(r => r.GetAll()).Returns(bugs);
            BugLogic bugLogic = new BugLogic(mock.Object, stateMock.Object, projectMock.Object);

            List<Bug> bugsSaved = bugLogic.GetBugsByProject(bug.Project.Name);

            mock.VerifyAll();
            Assert.IsTrue(bugsSaved.SequenceEqual(bugs));
        }
    }
}
