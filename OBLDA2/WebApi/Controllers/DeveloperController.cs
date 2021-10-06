using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using WebApi.Filters;
using OBLDA2.Models;
using System.Net;
using System;
using Domain;

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
            List<Bug> bugs = this.developerLogic.GetAllBugs(id);
            IEnumerable<BugEntryOutModel> bugsModel = BugEntryOutModel.ListBugs(bugs);

            return (StatusCode((int)HttpStatusCode.OK, bugsModel));
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
            developerLogic.DeleteDeveloperInProject(idProject, idDeveloper);
            return NoContent();
        }

        [HttpGet("{id}/CountBugsResolved")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult GetCountBugsResolvedByDeveloper(Guid id)
        {
            int countBugsResolved = this.developerLogic.CountBugDoneByDeveloper(id);
            return (StatusCode((int)HttpStatusCode.OK, countBugsResolved));
        }

        [HttpPut("{developerId}/UpdateBugState")]
        [AuthorizationFilter(Autorization.AllAutorization)]
        public IActionResult UpdateStateBug(Guid developerId, BugUpdateStateModel updateState)
        {
            this.developerLogic.UpdateState(updateState.BugId, 
                updateState.State, developerId);

            return NoContent();
        }

    }
}