using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System;
using System.Collections.Generic;
using System.Net;
using WebApi.Filters;

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

        [HttpPost("{id}/AssignTesterToProject")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult AssignTester(ProjectEntryModel project, Guid id)
        {

            User tester = new User();
            tester.Id = id;

            testerLogic.AssignTesterToProject(project.ToEntity(), tester);
            return NoContent();
        }

        [HttpDelete("{id}/DeleteTesterToProject")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult DeleteTester(Guid id, Project project)
        {
            User tester = new User();
            tester.Id = id;

            testerLogic.DeleteTesterInProject(project, tester);
            return NoContent();
        }

    }
}