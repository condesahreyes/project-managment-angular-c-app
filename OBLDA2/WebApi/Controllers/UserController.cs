using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using OBLDA2.Models;
using BusinessLogicInterface;
using System.Net;
using WebApi.Filters;

namespace OBLDA2.Controllers
{
    [Route("penguin/users")]
    [AuthorizationFilter(Autorization.Administrator)]
    public class UserController : ApiBaseController
    {
        private readonly IUserLogic userLogic;

        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserEntryModel userDTO)
        {
            User user = this.userLogic.Create(userDTO.ToEntity());
            UserOutModel userAdded = new UserOutModel(user);

            return (StatusCode((int)HttpStatusCode.Created, userAdded));

        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            IEnumerable<User> users = this.userLogic.GetAll();
            return Ok(users);
        }

        [HttpGet("{userID}")]
        public IActionResult GetById(Guid userID)
        {
            User userToReturn = this.userLogic.Get(userID);

            return Ok(new UserOutModel(userToReturn));
        }

    }
}