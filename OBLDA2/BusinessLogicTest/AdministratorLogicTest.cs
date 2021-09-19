using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using BusinessLogic;
using System.Linq;
using System;
using Domain;
using Moq;

namespace BusinessLogicTest
{
    [TestClass]
    public class AdministratorLogicTest
    {
        private Mock<IUserLogic> userLogicMock;
        private Mock<ITesterLogic> testerLogicMock;
        private Mock<IProjectLogic> projectMock;
        private Mock<IBugLogic> mockBug;

        private Rol rolAdministrator;
        private Rol rolTester;
        private Rol rolDeveloper;

        private AdministratorLogic administratorLogic;
        private User admin1;
        private User developer;
        private User tester;

        private Project project;
        
        private Bug bug;

        [TestInitialize]
        public void Setup()
        {
            userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            testerLogicMock = new Mock<ITesterLogic>(MockBehavior.Strict);
            projectMock = new Mock<IProjectLogic>(MockBehavior.Strict);
            mockBug = new Mock<IBugLogic>(MockBehavior.Strict);
            this.administratorLogic = new AdministratorLogic(userLogicMock.Object, projectMock.Object, testerLogicMock.Object, mockBug.Object);

            Guid id = new Guid();
            rolAdministrator = new Rol(id, "Administrator");
            rolTester = new Rol(id, "Tester");

            admin1 = new User(id, "Hernan", "reyes", "hernanReyes", "admin1234", "reyesH@gmail.com", rolAdministrator);
            project = new Project(id, "Project - GXC ");
            developer = new User(id, "Juan", "Gomez", "juanG", "juann245", "juan@gmail.com", rolDeveloper);
            bug = new Bug(project, 1, "Error de login", "Intento de sesión", "3.0", "Activo");
            tester = new User(id, "Fiorella", "Petrone", "fioPetro", "fio1245", "fiore@gmail.com", rolTester);
        }

        [TestMethod]
        public void CreateAdministratorOk()
        {
            userLogicMock.Setup(x => x.Create(admin1)).Returns(admin1);  
            var adminSaved = administratorLogic.Create(admin1);
            userLogicMock.VerifyAll();
            Assert.AreEqual(admin1, adminSaved);
        }

        [TestMethod]
        public void GetAllAdministrators()
        {
            List<User> list = new List<User>();
            list.Add(admin1);
            userLogicMock.Setup(x => x.GetAll()).Returns(list);
            IEnumerable<User> ret = administratorLogic.GetAll();
            userLogicMock.VerifyAll();
            Assert.IsTrue(ret.SequenceEqual(list));
        }

        [TestMethod]
        public void GetAdministratorByIdOk()
        {
            userLogicMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(admin1);

            var ret = administratorLogic.Get(admin1.Id);
            userLogicMock.VerifyAll();
            Assert.IsTrue(ret.Equals(admin1));
        }

        [ExpectedException(typeof(Exception), "The administrator doesn't exists")]
        [TestMethod]
        public void GetAdministratorByIdFail()
        {
            Guid id = Guid.NewGuid();
            User admin = null;
            userLogicMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(admin);
            var ret = administratorLogic.Get(id);
        }

        [TestMethod]
        public void CreateProjectOk()
        {
            projectMock.Setup(x => x.Create(project)).Returns(project);
            var projectSaved = administratorLogic.CreteProject(project);
            userLogicMock.VerifyAll();
            Assert.AreEqual(project, projectSaved);
        }

        [TestMethod]
        public void RemoveProjectOk()
        {
            projectMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(project);
            projectMock.Setup(m => m.Delete(project.Id));
            administratorLogic.DeleteProject(project.Id);
            Project projectNull = null;
            projectMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(projectNull);
            List<Project> list = new List<Project>();
            projectMock.Setup(x => x.GetAll()).Returns(list);
            var ret = administratorLogic.GetAllProject();
            userLogicMock.VerifyAll();
            Assert.IsTrue(ret.Count == 0);
        }

        [TestMethod]
        public void ProjectUpdateOk()
        {
            Guid id = Guid.NewGuid();
            var updatedProject = new Project(id, "Project Lab");
            projectMock.Setup(x => x.Update(project.Id, updatedProject)).Returns(updatedProject);
            var ret = administratorLogic.UpdateProject(project.Id, updatedProject);
            projectMock.VerifyAll();
            Assert.AreEqual(ret.Name, updatedProject.Name);
        }

        [TestMethod]
        public void DeleteTesterProject()
        {
            projectMock.Setup(m => m.DeleteTester(project, tester));
            administratorLogic.DeleteTesterByProject(project, tester);

            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllTesters(project)).Returns(list);
            var ret = administratorLogic.GetAllTesters(project);
            userLogicMock.VerifyAll();

            Assert.AreEqual(ret, list);
        }

        [TestMethod]
        public void DeleteDeveloperProject()
        {
            projectMock.Setup(m => m.DeleteDeveloper(project, developer));
            administratorLogic.DeleteDeveloperByProject(project, developer);

            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllDevelopers(project)).Returns(list);
            var ret = administratorLogic.GetAllDevelopers(project);
            userLogicMock.VerifyAll();

            Assert.AreEqual(ret, list);
        }

        [TestMethod]
        public void CreateBugByAdmin()
        {
            mockBug.Setup(x => x.Create(bug)).Returns(bug);

            var ret = administratorLogic.CreateBug(bug);
            mockBug.VerifyAll();

            Assert.IsTrue(ret.Id == bug.Id);
        }

        [TestMethod]
        public void UpdateBugProject()
        {
            var bugUpdate = new Bug(project, 3, "Bug login", "Intetno logOut", "4.1", "activo");
            mockBug.Setup(x => x.Update(bug.Id, bugUpdate)).Returns(bugUpdate);

            var ret = administratorLogic.UpdateBug(bug.Id , bugUpdate);
            mockBug.VerifyAll();

            Assert.AreEqual(ret.Name, bugUpdate.Name);
        }

        [TestMethod]
        public void DeleteBugProject()
        {
            mockBug.Setup(x => x.Get(bug.Id)).Returns(bug);
            mockBug.Setup(m => m.Delete(bug.Id));
            administratorLogic.DeleteBug(bug.Id);

            List<Project> list = new List<Project>();

            projectMock.Setup(x => x.GetAll()).Returns(list);

            var ret = administratorLogic.GetTotalBugByAllProject();
            userLogicMock.VerifyAll();

            Assert.IsTrue(ret == 0);
        }

        [TestMethod]
        public void AssignDeveloperProject()
        {
            projectMock.Setup(x => x.AssignDeveloper(project, developer));
            administratorLogic.AssignDeveloperByProject(project, developer);

            List<User> list = new List<User>();
            list.Add(developer);

            projectMock.Setup(x => x.GetAllDevelopers(project)).Returns(list);
            var ret = administratorLogic.GetAllDevelopers(project);
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);
        }

        [TestMethod]
        public void AssignTesterProject()
        {
            projectMock.Setup(x => x.AssignTester(project, tester));
            administratorLogic.AssignTesterByProject(project, tester);

            List<User> list = new List<User>();
            list.Add(tester);

            projectMock.Setup(x => x.GetAllTesters(project)).Returns(list);
            var ret = administratorLogic.GetAllTesters(project);
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);
        }

        [TestMethod]
        public void ImportBugsProjectByProvider()
        {

            //Ver despues 
        }

        [TestMethod]
        public void GetAllBugInAllProjects()
        {
            Guid id = new Guid();
            Project pro = new Project(id,"New");
            pro.totalBugs = 5;

            List<Project> list = new List<Project>();
            list.Add(pro);

            projectMock.Setup(x => x.GetAll()).Returns(list);
            var ret = administratorLogic.GetTotalBugByAllProject();
            projectMock.VerifyAll();

            Assert.AreEqual(ret, 5);
        }

        [TestMethod]
        public void GetFixedBugsDeveloper()
        {
            bug.SolvedBy = developer;
            List<Bug> bugs = new List<Bug>();
            bugs.Add(bug);

            mockBug.Setup(x => x.GetAll()).Returns(bugs);

            var ret = administratorLogic.GetFixedBugsByDeveloper(developer.Id);
            projectMock.VerifyAll();

            Assert.AreEqual(bugs.Count, ret);
        }

    }
}
