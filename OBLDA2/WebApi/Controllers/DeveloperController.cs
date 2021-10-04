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
        
        [HttpPost("{id}/AssignDeveloperToProject")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult AssignDeveloperToProject(ProjectEntryModel project, Guid id)
        {
            User developer = new User();
            developer.Id = id;

            developerLogic.AssignDeveloperToProject(project.ToEntity(), developer);
            return NoContent();
        }

        [HttpDelete("{id}/DeleteProject")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult DeleteDeveloperToProject(Guid id, Project project)
        {
            User developer = new User();
            developer.Id = id;
            try
            {
                developerLogic.DeleteDeveloperInProject(project, developer);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Project not found or Tester not found");
            }
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