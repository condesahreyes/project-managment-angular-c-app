using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using OBLDA2.Models;
using BusinessLogicInterface;
using System.Net;

namespace OBLDA2.Controllers
{
    [Route("penguin/sessions")]
    //aca iria el filtro para la autenticacion
    public class SessionController : ApiBaseController
    {
        private ISessionLogic sessionsLogic;

        public SessionController(ISessionLogic sessions) : base()
        {
            sessionsLogic = sessions;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginEntryModel model)
        {
            var token = sessionsLogic.Login(model.Email, model.Password);
            return Ok(new LoginOutModel { Token = token });
        }

        [HttpPost("logout")]
        //[ServiceFilter(typeof(AuthorizationAttributeFilter))]
        public IActionResult Logout([FromBody] LogoutEntryModel model)
        {
            sessionsLogic.Logout(model.Token);
            return Ok(new LogoutOutModel());
        }

    }
}