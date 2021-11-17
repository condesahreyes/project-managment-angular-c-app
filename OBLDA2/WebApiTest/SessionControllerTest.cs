using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using WebApi.Controllers;
using OBLDA2.Models;
using System.Linq;
using Domain;
using System;
using Moq;
using OBLDA2.Controllers;
using Exceptions;
using Castle.Core.Internal;

namespace WebApiTest
{
    [TestClass]
    public class SessionControllerTest
    {

        private string email = "hreyes@gmail.com";
        private string password = "123123";
        private string token = "Administrador-011bad2f-bc54-4a7b-a8ac-21ad9aab5fa9";
        private string tokenFail = "deve-af193e74-e115-4f5f-8600-3398d9a75f96";

        [TestMethod]
        public void CorrectLoginTest()
        {
            var loginModel = new LoginEntryModel
            {
                Email = email,
                Password = password
            };

            var mock = new Mock<ISessionLogic>();
            mock.Setup(s => s.Login(email, password)).Returns(token);
            var sessionController = new SessionController(mock.Object);

            IActionResult result = sessionController.Login(loginModel);
            var status = result as OkObjectResult;
            var loginOutModel = status.Value as LoginOutModel;

            mock.VerifyAll();
            Assert.AreEqual(token, loginOutModel.Token);
        }


        [TestMethod]
        public void LogoutTest()
        {
            var logoutModel = new LogoutEntryModel
            {
                Token = token,
            };

            var mock = new Mock<ISessionLogic>();
            mock.Setup(s => s.Logout(token));
            var sessionController = new SessionController(mock.Object);

            IActionResult result = sessionController.Logout(logoutModel);
            var status = result as OkObjectResult;
            var logoutModelResult = status.Value as LogoutOutModel;

            mock.VerifyAll();
            Assert.AreEqual(200, status.StatusCode);
            Assert.IsFalse(logoutModelResult.Message.IsNullOrEmpty());
        }
    }
}
