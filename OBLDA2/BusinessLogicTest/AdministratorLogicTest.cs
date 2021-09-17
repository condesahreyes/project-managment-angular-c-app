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
<<<<<<< HEAD
        private Rol rolAdministrator;
        private Mock<IAdministratorLogic> Mock;
        private Mock<IRepository<User,Guid>> daMock;
        private Mock<IRepository<Project, Guid>> projectMock;
        ProjectLogic projectLogic;
        AdministratorLogic administratorLogic;
        User admin1;
        Project project;

        [TestInitialize]
        public void Setup()
        {
            Guid idRol = new Guid();
            rolAdministrator = new Rol(idRol, "Administrator");
            daMock = new Mock<IRepository<User,Guid>>(MockBehavior.Strict);
            projectMock = new Mock<IRepository<Project,Guid>>(MockBehavior.Strict);
            Mock = new Mock<IAdministratorLogic>(MockBehavior.Strict);
            this.administratorLogic = new AdministratorLogic(daMock.Object);
            this.projectLogic = new ProjectLogic(projectMock.Object);

            Guid id = new Guid();
            admin1 = new User(id, "Hernan", "reyes", "hernanReyes", "admin1234", "reyesH@gmail.com", rolAdministrator);
            project = new Project(id, "Project - GXC ");
        }
=======

>>>>>>> TesterLogicTest

        [ExpectedException(typeof(Exception), "The name can not be empty")]
        [TestMethod]
        public void NonEmptyName()
        {
            Guid id = new Guid();
            var admin = new User(id, "", "Asadurian", "diegoAsa", "admin1234", "diegoasadurian@gmail.com", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save()); 
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The lastName can not be empty")]
        [TestMethod]
        public void NonEmptyLastName()
        {
            Guid id = new Guid();
            var admin = new User(id, "Diego", "", "diegoAsa", "admin1234", "diegoasadurian@gmail.com", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save()); 
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The userName can not be empty")]
        [TestMethod]
        public void NonEmptyUserName()
        {
            Guid id = new Guid();
            var admin = new User(id, "Diego", "Asadurian", "", "admin1234", "diegoasadurian@gmail.com", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save()); 
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The password can not be empty")]
        [TestMethod]
        public void NonEmptyPassword()
        {
            Guid id = new Guid();
            var admin = new User(id, "Diego", "Asadurian", "diegoAsa", "", "diegoasadurian@gmail.com", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save());
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The email can not be empty")]
        [TestMethod]
        public void NonEmptyEmail()
        {
            Guid id = new Guid();
            var admin = new User(id, "Diego", "Asadurian", "diegoAsa", "adc1234", "", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save());
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The password must contains > 6 caracteres")]
        [TestMethod]
        public void CreateAdministratorInvalidPass()
        {
            Guid id = new Guid();
            var admin = new User(id, "Diego", "Asadurian", "adcs", "adc1234", "diego@gmail.com", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save()); 
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The email must be correct")]
        [TestMethod]
        public void CreateAdministratorInvalidEmail()
        {
            Guid id = new Guid();
            var admin = new User(id, "Diego", "Asadurian", "adcs", "adc1234", "diegogmail.com", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save()); 
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The password must contains > 6 caracteres")]
        [TestMethod]
        public void CreateAdministratorWithLongPass()
        {
            Guid id = new Guid();
            var admin = new User(id, "Diego", "Asadurian", "adcs", "adc123dasdasdasdasd4", "diego@gmail.com", rolAdministrator);
            daMock.Setup(x => x.Create(admin)).Verifiable();
            daMock.Setup(x => x.Save()); 
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin);
            daMock.VerifyAll();
        }

        [ExpectedException(typeof(Exception), "The administrator already exist")]
        [TestMethod]
        public void CreateAdministratorAreadyExists()
        {
            daMock.Setup(x => x.Create(admin1)).Verifiable();
            daMock.Setup(x => x.Save()); 
            List<User> list = new List<User>();
            daMock.Setup(x => x.GetAll()).Returns(list);
            var adminSaved = administratorLogic.Create(admin1);
            daMock.VerifyAll();
        }

        [TestMethod]
        public void CreateAdministratorOk()
        {

            daMock.Setup(x => x.Create(admin1)).Verifiable();
            daMock.Setup(x => x.Save());   
            var adminSaved = administratorLogic.Create(admin1);
            daMock.VerifyAll();
            Assert.AreEqual(admin1, adminSaved);
        }

        [TestMethod]
        public void GetAllAdministrators()
        {

            List<User> list = new List<User>();
            list.Add(admin1);
            daMock.Setup(x => x.GetAll()).Returns(list);
            IEnumerable<User> ret = administratorLogic.GetAll();
            daMock.VerifyAll();
            Assert.IsTrue(ret.SequenceEqual(list));
        }

       
        [TestMethod]
        public void GetAdministratorByIdOk()
        {
            daMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(admin1);

            var ret = administratorLogic.Get(admin1.Id);
            daMock.VerifyAll();
            Assert.IsTrue(ret.Equals(admin1));

        }

        [ExpectedException(typeof(Exception), "The administrator doesn't exists")]
        [TestMethod]
        public void GetAdministratorByIdFail()
        {
            Guid id = Guid.NewGuid();
            User admin = null;
            daMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(admin);
            var ret = administratorLogic.Get(id);
            Assert.IsFalse(ret.Equals(admin));

        }

        [TestMethod]
        public void CreateProjectOk()
        {
            projectMock.Setup(x => x.Create(project)).Verifiable();
            projectMock.Setup(x => x.Save());
            var projectSaved = administratorLogic.CreteProject(project);
            daMock.VerifyAll();
            Assert.AreEqual(project, projectSaved);
        }

        [TestMethod]
        public void RemoveProjectOk()
        {
            projectMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(project);
            projectMock.Setup(m => m.Delete(project.Id));
            projectMock.Setup(m => m.Save());
            administratorLogic.DeleteProject(project.Id);
            Project projectNull = null;
            projectMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(projectNull);
            var ret = projectLogic.Get(project.Id);
            daMock.VerifyAll();
            Assert.IsFalse(ret.Equals(projectNull));

        }

        [TestMethod]
        public void ProjectUpdateOk()
        {
            projectMock.Setup(x => x.Get(It.IsAny<Guid>())).Returns(project);
            Guid id = Guid.NewGuid();
            var updatedProject = new Project(id,"Project Lab");
            projectMock.Setup(x => x.Update(id, updatedProject));
            projectMock.Setup(x => x.Save());
            administratorLogic.UpdateProject(id, updatedProject); 
            daMock.VerifyAll();
            Assert.AreEqual(updatedProject.Name, "Project Lab");

        }


    }
}
