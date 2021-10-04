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
            User user = new User();
            user.Id = id;

            IEnumerable<Bug> bugs = this.testerLogic.GetAllBugs(user);

            return (StatusCode((int)HttpStatusCode.OK, bugs));
        }

        [HttpPost("{idTester}/AssignTesterToProject/{projectId}")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult AssignTester(Guid projectId, Guid idTester)
        {
            User tester = new User();
            tester.Id = idTester;

            Project project = new Project();
            project.Id = projectId;

            testerLogic.AssignTesterToProject(project, tester);
            return NoContent();
        }

        [HttpDelete("{idTester}/DeleteTesterToProject/{projectId}")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult DeleteTester(Guid idTester, Guid projectId)
        {
            User tester = new User();
            tester.Id = idTester;

            Project project = new Project();
            project.Id = projectId;

            testerLogic.DeleteTesterInProject(project, tester);
            return NoContent();
        }

    }
}