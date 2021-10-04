using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Net;
using System;
using Domain;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("penguin/developers")]
    public class DeveloperController : ApiBaseController
    {
        private readonly IDeveloperLogic developerLogic;

        public DeveloperController(IDeveloperLogic developerLogic)
        {
            this.developerLogic = developerLogic;
        }

        [HttpGet("{id}/DeveloperGetAllBugs")]
        [AuthorizationFilter(Autorization.DeveloperAndAdmin)]
        public IActionResult GetAllBugsDeveloper(Guid id)
        {
            User developer = new User();
            developer.Id = id;

            IEnumerable<Bug> bugs = this.developerLogic.GetAllBugs(developer);

            return (StatusCode((int)HttpStatusCode.OK, bugs));
        }
        
        [HttpPost("{idDeveloper}/AssignDeveloperToProject/{idProject}")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult AssignDeveloperToProject(Guid idProject, Guid idDeveloper)
        {
            User developer = new User();
            developer.Id = idDeveloper;

            Project project = new Project();
            project.Id = idProject;

            developerLogic.AssignDeveloperToProject(project, developer);
            return NoContent();
        }

        [HttpDelete("{idDeveloper}/DeleteProject/{idProject}")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult DeleteDeveloperToProject(Guid idDeveloper, Guid idProject)
        {
            User developer = new User();
            developer.Id = idDeveloper;

            Project project = new Project();
            project.Id = idProject;

            developerLogic.DeleteDeveloperInProject(project, developer);
            return NoContent();
        }

        [HttpGet("{id}/CountBugsResolved")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult GetCountBugsResolvedByDeveloper(Guid id)
        {
            User developer = new User();
            developer.Id = id;

            int countBugsResolved = this.developerLogic.CountBugDoneByDeveloper(developer);

            return (StatusCode((int)HttpStatusCode.OK, countBugsResolved));
        }

    }
}