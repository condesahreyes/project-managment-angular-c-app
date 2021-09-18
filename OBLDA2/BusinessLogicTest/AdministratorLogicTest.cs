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
    public class AdministratorLogicTest
    {
        private Mock<IUserLogic> userLogicMock;
        private Mock<ITesterLogic> testerLogicMock;
        private Mock<IProjectLogic> projectMock;

        private Rol rolAdministrator;
        private Rol rolTester;
        private Rol rolDeveloper;

        private AdministratorLogic administratorLogic;
        private User admin1;
        private User developer;
        private Project project;
        private User tester;

        [TestInitialize]
        public void Setup()
        {
            userLogicMock = new Mock<IUserLogic>(MockBehavior.Strict);
            testerLogicMock = new Mock<ITesterLogic>(MockBehavior.Strict);
            projectMock = new Mock<IProjectLogic>(MockBehavior.Strict);
            this.administratorLogic = new AdministratorLogic(userLogicMock.Object, projectMock.Object, testerLogicMock.Object);

            Guid id = new Guid();
            rolAdministrator = new Rol(id, "Administrator");
            rolTester = new Rol(id, "Tester");

            admin1 = new User(id, "Hernan", "reyes", "hernanReyes", "admin1234", "reyesH@gmail.com", rolAdministrator);
            project = new Project(id, "Project - GXC ");
            developer = new User(id, "Juan", "Gomez", "juanG", "juann245", "juan@gmail.com", rolDeveloper);
            tester = new User(id, "Fiorella", "Petrone", "fioPetro", "fio1245", "fiore@gmail.com", rolTester);
        }

        [TestMethod]
        public void CreateAdministratorOk()
        {

            userLogicMock.Setup(x => x.Create(admin1)).Returns(admin1);
            //userLogicMock.Setup(x => x.Save());   
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
           /* projectMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(project);
            testerLogicMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(tester);
           */
            projectMock.Setup(m => m.DeleteTester(project, tester.Id));
            administratorLogic.DeleteTesterByProject(project, tester.Id);

           /* User testerNull = null;
            testerLogicMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(testerNull);
            */
            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllTesters()).Returns(list);
            var ret = administratorLogic.GetAllTesters();
            userLogicMock.VerifyAll();

            Assert.AreEqual(ret, list);

        }

        [TestMethod]
        public void DeleteDeveloperProject()
        {
            /* projectMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(project);
           testerLogicMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(tester);
          */
            projectMock.Setup(m => m.DeleteTester(project, developer.Id));
            administratorLogic.DeleteTesterByProject(project, developer.Id);

            /* User testerNull = null;
             testerLogicMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(testerNull);
             */
            List<User> list = new List<User>();
            projectMock.Setup(x => x.GetAllDevelopers()).Returns(list);
            var ret = administratorLogic.GetAllDevelopers();
            userLogicMock.VerifyAll();

            Assert.AreEqual(ret, list);

        }
        [TestMethod]
        public void CreteBugProject()
        {


        }

        [TestMethod]
        public void UpdateBugProject()
        {


        }


        [TestMethod]
        public void DeleteBugProject()
        {


        }

        [TestMethod]
        public void AssignDeveloperProject()
        {
            projectMock.Setup(x => x.AssignDeveloper(project, developer.Id));
            administratorLogic.AssignDeveloperByProject(project, developer.Id);

            List<User> list = new List<User>();
            list.Add(developer);

            projectMock.Setup(x => x.GetAllDevelopers()).Returns(list);
            var ret = administratorLogic.GetAllDevelopers();
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);

        }

        [TestMethod]
        public void AssignTesterProject()
        {
            projectMock.Setup(x => x.AssignTester(project, tester.Id));
            administratorLogic.AssignTesterByProject(project, tester.Id);

            List<User> list = new List<User>();
            list.Add(tester);

            projectMock.Setup(x => x.GetAllTesters()).Returns(list);
            var ret = administratorLogic.GetAllTesters();
            projectMock.VerifyAll();

            Assert.AreEqual(ret, list);

        }

        [TestMethod]
        public void ImportBugsProjectByProvider()
        {


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
            int fixedBugs = 4; 
            projectMock.Setup(x => x.GetAllFixedBugsByDeveloper(developer.Id)).Returns(fixedBugs);

            var ret = administratorLogic.GetFixedBugsByDeveloper(developer.Id);
            projectMock.VerifyAll();

            Assert.AreEqual(fixedBugs, ret);

        }


    }
}
