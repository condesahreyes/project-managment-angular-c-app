using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using WebApi.Filters;
using OBLDA2.Models;
using System.Net;
using System;
using Domain;

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

            return (StatusCode((int)HttpStatusCode.Created, new UserOutModel(user)));

        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            List<User> users = this.userLogic.GetAll();

            return Ok(UserOutModel.ListUser(users));
        }

        [HttpGet("{userID}")]
        public IActionResult GetById(Guid userID)
        {
            User userToReturn = this.userLogic.Get(userID);

            return Ok(new UserOutModel(userToReturn));
        }

    }
}