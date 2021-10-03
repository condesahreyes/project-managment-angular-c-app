using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Net;
using System.Linq;

namespace WebApi.Controllers
{

    [Route("Penguin/bugs")]
    public class BugController : ApiBaseController
    {
        private readonly IBugLogic bugLogic;

        public BugController(IBugLogic bugLogic)
        {
            this.bugLogic = bugLogic;
        }

    }
}