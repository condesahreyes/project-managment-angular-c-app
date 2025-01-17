﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using BusinessLogicInterface;
using Imports;
using Domain;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogicTest.BugsImportTest
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BugsImportTest
    {
        private static State activeStatus = new State(State.active);
        private static State doneStatus = new State(State.done);

        private static Project project = new Project("Proyecto en JSON");

        private List<Bug> bugs = new List<Bug>{
            new Bug(project, 1, "nombre1", "dominio1", "V 1.0", activeStatus, 0),
            new Bug(project, 2, "nombre2", "dominio2", "V 2.0", doneStatus, 0)
        };

        private Mock<IBugLogic> bugLogic;
        private BugsImport bugsImport;

        [TestInitialize]
        public void Setup()
        {
            bugLogic = new Mock<IBugLogic>(MockBehavior.Strict);
            bugsImport = new BugsImport(bugLogic.Object);
        }

        [TestMethod]
        public void CreateImportBugs()
        {
            bugLogic.Setup(x => x.Create(bugs[0])).Returns(bugs[0]);
            bugLogic.Setup(x => x.Create(bugs[1])).Returns(bugs[1]);

            List<Bug> savedImportedBugs = bugsImport.CreateBugs(bugs);

            bugLogic.VerifyAll();
            CollectionAssert.AreEqual(savedImportedBugs, bugs);
        }

    }
}
