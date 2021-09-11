using System;
using System.Collections.Generic;
using System.Linq;
using BusinessLogic.UserRol;
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class TesterLogicTest
    {

        private Mock<ITesterLogic> Mock;
        private Mock<IRepository<Tester>> daMock;
        private Mock<IRepository<Bug>> bugMock;


        private Mock<IRepository<Project>> daMockProject;
        private Mock<IProjectLogic> mockProjectLogic;

        BugLogic bugLogic;
        TesterLogic testerLogic;
        Tester tester;
        Project project;
        Bug bug;
        ProjectLogic projectLogic;

        [TestInitialize]
        public void Setup()
        {
            daMock = new Mock<IRepository<Tester>>(MockBehavior.Strict);
            daMockProject = new Mock<IRepository<Project>>(MockBehavior.Strict);
            mockProjectLogic = new Mock<IProjectLogic>(MockBehavior.Strict);

            Mock = new Mock<ITesterLogic>(MockBehavior.Strict);
            this.testerLogic = new TesterLogic(daMock.Object);
            Guid id = Guid.NewGuid();
            var tester = new Tester(id, "diego", "asadurian", "diegoA", "adc1234", "diego@test.com");
            var project = new Project(id,"GXC-Test");
            var bag = new Bug("Test-LMN","Error en login", "Test", "V 1.2", "Fail" );
        }

        [TestMethod]
        public void GetAllTesters()
        {
            List<Tester> list = new List<Tester>();
            list.Add(tester);
            daMock.Setup(x => x.GetAll()).Returns(list);

            IEnumerable<Tester> ret = testerLogic.GetAll();
            daMock.VerifyAll();
            Assert.IsTrue(ret.SequenceEqual(list));
        }


        [TestMethod]
        public void GetTesterIdOk()
        {
            Guid id = Guid.NewGuid();
            daMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(tester);
            var ret = testerLogic.Get(id);
            daMock.VerifyAll();
            Assert.IsTrue(ret.Equals(tester));

        }

        [ExpectedException(typeof(Exception), "The tester doesn't exists")]
        [TestMethod]
        public void GetTesterByIdFail()
        {
            Guid id = Guid.NewGuid();
            Tester test = null;
            daMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(test);
            var ret = testerLogic.Get(id);
            Assert.IsFalse(ret.Equals(test));

        }

        [TestMethod]
        public void CreateTesterOk()
        {
            daMock.Setup(x => x.Create(tester)).Verifiable();
            daMock.Setup(x => x.Save());
            var ret = testerLogic.Create(tester);
            daMock.VerifyAll();
            Assert.AreEqual(ret, tester);

        }

        [TestMethod]
        public void GetAllPtojectsByTester()
        {
            List<Project> projects = new List<Project>();
            projects.Add(project);
            //daMockProject.Setup(x => x.GetAll()).Returns(projects);
            Mock.Setup(x => x.getProjectsByTester(tester.Id)).Returns(projects);

            IEnumerable<Project> projectByTester = testerLogic.getProjectsByTester(tester.Id);

            daMock.VerifyAll();
            Assert.IsTrue(projects.SequenceEqual(projectByTester));
        }

        [TestMethod]
        public void GetAllBugsByTester()
        {
            List<Bug> bugs = new List<Bug>();
            bugs.Add(bug);
            //daMockProject.Setup(x => x.GetAll()).Returns(projects);
            Mock.Setup(x => x.getBugsByTester(tester.Id, project.Id, bug.Name, bug.State)).Returns(bugs); //aca no iria el project Id
            IEnumerable<Bug> bugsByTester = testerLogic.getBugsByTester(tester.Id, project.Id, bug.Name, bug.State);
            daMock.VerifyAll();
            Assert.IsTrue(bugs.SequenceEqual(bugsByTester));
        }

        [TestMethod]
        public void CreateBugByTester()
        {
            bugMock.Setup(x => x.Create(bug)).Verifiable();
            bugMock.Setup(x => x.Save());
            var ret = testerLogic.CreateBug(project.Id, "Test-LMN", "Error en login", "Test", "V 1.2", "Fail");
            daMock.VerifyAll();
            Assert.AreEqual(ret, bug);
        }

        [TestMethod]
        public void DeleteBugByTester()
        {
            bugMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(bug);
            bugMock.Setup(m => m.Delete(bug));
            daMock.Setup(m => m.Save());
            bugLogic.Delete(bug.Id);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The bug doesn't exists")]
        [TestMethod]
        public void RemoveBugFail()
        {
            Guid id = Guid.NewGuid();
            Bug bugToDelete = null;

            bugMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(bugToDelete);
            bugMock.Setup(m => m.Delete(bugToDelete));
            bugMock.Setup(m => m.Save());
            bugLogic.Delete(Guid.NewGuid());
            var ret = bugLogic.Get(id);
            Assert.IsFalse(ret.Equals(bugToDelete));
        }

        [ExpectedException(typeof(Exception), "The tester exists")]
        [TestMethod]
        public void CreateTesterFailAlreadyExists()
        {
            daMock.Setup(x => x.Create(tester)).Verifiable();
            daMock.Setup(x => x.Save());
            var ret = testerLogic.Create(tester);  
            
        }
    }
}
