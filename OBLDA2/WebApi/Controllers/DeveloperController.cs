using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Controllers
{
    [Route("Penguin/testers")]
    public class DeveloperController : ApiBaseController
    {
        private IDeveloperLogic developerLogic;

        public DeveloperController(IDeveloperLogic developerLogic)
        {
            this.developerLogic = developerLogic;
        }

    }
}