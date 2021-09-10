using BusinessLogic;
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

        private Mock<IAdministratorLogic> Mock;
        private Mock<IRepository<User>> daMock;
        AdministratorLogic administratorLogic;


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
