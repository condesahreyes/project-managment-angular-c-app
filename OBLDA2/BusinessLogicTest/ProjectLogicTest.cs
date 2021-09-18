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
        private Mock<IProjectLogic> Mock;

        private ProjectLogic projectLogic;
        private Project project;

        [TestInitialize]
        public void Setup()
        {
            mock = new Mock<IRepository<Project,Guid>>(MockBehavior.Strict);
            Mock = new Mock<IProjectLogic>(MockBehavior.Strict);
            this.projectLogic = new ProjectLogic(mock.Object);

            Guid id = new Guid();
            project = new Project(id, "Project - Pineapple ");
        }

        [TestMethod]
        public void CreateProjectOk()
        {

            mock.Setup(x => x.Create(project)).Returns(project);
            var projectSaved = projectLogic.Create(project);
            mock.VerifyAll();
            Assert.AreEqual(project, projectSaved);
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
        

    }
}
