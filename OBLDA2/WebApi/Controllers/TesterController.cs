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

        [HttpGet("{idTester}/bugs")]
        [AuthorizationFilter(Autorization.Tester)]
        public IActionResult GetAllBugsTester(Guid idTester)
        {
            List<Bug> bugs = this.testerLogic.GetAllBugs(idTester);

            IEnumerable<BugEntryOutModel> bugsModel = BugEntryOutModel.ListBugs(bugs);

            return (StatusCode((int)HttpStatusCode.OK, bugsModel));
        }

        [HttpPost("{idTester}/project/{idProject}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult AssignTester(Guid idProject, Guid idTester)
        {
            testerLogic.AssignTesterToProject(idProject, idTester);
            return NoContent();
        }

        [HttpDelete("{idTester}/project/{idProject}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult DeleteTester(Guid idTester, Guid idProject)
        {
            testerLogic.DeleteTesterInProject(idProject, idTester);
            return NoContent();
        }

    }
}