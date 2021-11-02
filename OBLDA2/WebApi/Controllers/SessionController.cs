using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Models;
using Domain;
using System.Collections.Generic;
using System;

namespace OBLDA2.Controllers
{
    [Route("penguin/sessions")]
    public class SessionController : ApiBaseController
    {
        private ISessionLogic sessionsLogic;
        private IUserLogic userLogic;

        public SessionController(ISessionLogic sessions, IUserLogic usuLogic) : base()
        {
            sessionsLogic = sessions;
            userLogic = usuLogic;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginEntryModel model)
        {
            var token = sessionsLogic.Login(model.Email, model.Password);
            return Ok(new LoginOutModel { Token = token });
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutEntryModel model)
        {
            sessionsLogic.Logout(model.Token);
            return Ok(new LogoutOutModel());
        }

        [HttpGet("{userToken}")]
        public IActionResult GetUserLogged(string userToken)
        {
            string userId = sessionsLogic.GetUserIdWithToekn(userToken);
            return Ok(new UserIdModel(Guid.Parse(userId)));
        }

    }
}