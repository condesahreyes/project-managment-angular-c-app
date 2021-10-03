using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Net;
using Domain;
using System;

namespace WebApi.Controllers
{
    [Route("Penguin/projects")]
    public class ProjectController : ApiBaseController
    {
        private readonly IProjectLogic projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            this.projectLogic = projectLogic;
        }

    }
}