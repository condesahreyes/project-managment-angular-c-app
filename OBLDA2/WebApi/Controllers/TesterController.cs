using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using WebApi.Filters;
using OBLDA2.Models;
using System.Net;
using Domain;
using System;

namespace WebApi.Controllers
{
    [Route("penguin/testers")]
    public class TesterController : ApiBaseController
    {
        private readonly ITesterLogic testerLogic;

        public TesterController(ITesterLogic testerLogic)
        {
            this.testerLogic = testerLogic;
        }

        [HttpGet("{id}/GetAllBugsForTester")]
        [AuthorizationFilter(Autorization.Tester)]
        public IActionResult GetAllBugsTester(Guid id)
        {
            List<Bug> bugs = this.testerLogic.GetAllBugs(id);

            IEnumerable<Bug> bugsModel = (IEnumerable<Bug>)BugEntryOutModel.ListBugs(bugs);

            return (StatusCode((int)HttpStatusCode.OK, bugsModel));
        }

        [HttpPost("{idTester}/AssignTesterToProject/{projectId}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult AssignTester(Guid projectId, Guid idTester)
        {
            testerLogic.AssignTesterToProject(projectId, idTester);
            return NoContent();
        }

        [HttpDelete("{idTester}/DeleteTesterToProject/{projectId}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult DeleteTester(Guid idTester, Guid projectId)
        {
            testerLogic.DeleteTesterInProject(projectId, idTester);
            return NoContent();
        }

    }
}