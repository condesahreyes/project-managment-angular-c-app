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
            IEnumerable<Bug> bugs = this.developerLogic.GetAllBugs(id);

            return (StatusCode((int)HttpStatusCode.OK, bugs));
        }
        
        [HttpPost("{idDeveloper}/AssignDeveloperToProject/{idProject}")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult AssignDeveloperToProject(Guid idProject, Guid idDeveloper)
        {
            developerLogic.AssignDeveloperToProject(idProject, idDeveloper);
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

            developerLogic.DeleteDeveloperInProject(project.Id, developer.Id);
            return NoContent();
        }

        [HttpGet("{id}/CountBugsResolved")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult GetCountBugsResolvedByDeveloper(Guid id)
        {
            User developer = new User();
            developer.Id = id;

            int countBugsResolved = this.developerLogic.CountBugDoneByDeveloper(developer.Id);

            return (StatusCode((int)HttpStatusCode.OK, countBugsResolved));
        }

        [HttpPut("{developerId}/UpdateBugState")]
        [AuthorizationFilter(Autorization.AllAutorization)]
        public IActionResult UpdateStateBug(Guid developerId, BugUpdateStateModel updateState)
        {
            Bug bugReturn = this.developerLogic.UpdateState(updateState.BugId, updateState.State, developerId);
            return NoContent();
        }

    }
}