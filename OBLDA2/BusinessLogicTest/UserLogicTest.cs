using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogicTest
{
    public class UserLogicTest
    {
        [TestMethod]
        public void IsValidName()
        {
            string validName = "Hernán";
            Assert.IsTrue(UserLogic.ValidateUser(validName) == validName);
        }

        [TestMethod]
        public void IsValidSurname()
        {
            string validSurname = "Reyes Condesa";
            Assert.IsTrue(UserLogic.ValidateSurname(validSurname) == validSurname);
        }

        [TestMethod]
        public void IsValidUserName()
        {
            string validUserName = "hreyes99";
            Assert.IsTrue(UserLogic.ValidateUserName(validUserName) == validUserName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidName()
        {
            UserLogic.ValidateUser("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidSurname()
        {
            UserLogic.ValidateSurname("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidUserName()
        {
            UserLogic.ValidateUserName("");
        }

        [TestMethod]
        public void IsValidEmail()
        {
            string validEmail = "hernan@ort.edu.uy";
            Assert.IsTrue(UserLogic.ValidateEmail(validEmail) == validEmail);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IsNotValidEmail()
        {
            string invalidEmail = "hernanort.edu.uy";
            UserLogic.ValidateUserName(invalidEmail);
        }
    }
}
