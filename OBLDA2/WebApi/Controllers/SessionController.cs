using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Models;
using Domain;

namespace OBLDA2.Controllers
{
    [Route("penguin/sessions")]
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
        public IActionResult Logout([FromBody] LogoutEntryModel model)
        {
            sessionsLogic.Logout(model.Token);
            return Ok(new LogoutOutModel());
        }

        [HttpGet("{userToken}")]
        public IActionResult GetUserLogged(string userToken)
        {
            User user = sessionsLogic.GetUserWithToekn(userToken);
            return Ok(new UserOutModel(user));
        }

    }
}