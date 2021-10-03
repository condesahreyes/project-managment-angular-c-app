using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using OBLDA2.Models;
using BusinessLogicInterface;
using System.Net;

namespace OBLDA2.Controllers
{
    [Route("Penguin/users")]
    //aca iria el filtro para la autenticacion
    public class UserController : ApiBaseController
    {
        private readonly IUserLogic userLogic;

        public UserController(IUserLogic userLogic)
        {
            this.userLogic = userLogic;
        }

        

    }
}