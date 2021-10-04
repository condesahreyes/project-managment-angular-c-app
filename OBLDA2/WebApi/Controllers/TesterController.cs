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
        [AuthorizationFilter(Rol.tester)]
        public IActionResult GetAllBugsTester(Guid id)
        {
            User user = new User();
            user.Id = id;

            IEnumerable<Bug> bugs = this.testerLogic.GetAllBugs(user);

            return (StatusCode((int)HttpStatusCode.OK, bugs));

        }

        [HttpPost("{id}/AssignTesterToProject")]
        [AuthorizationFilter(Rol.administrator)]

        public IActionResult AssignTester(ProjectEntryModel project, Guid id)
        {

            User tester = new User();
            tester.Id = id;

            try
            {
                testerLogic.AssignTesterToProject(project.ToEntity(), tester);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Project not found or Tester not found");
            }

        }

        [HttpDelete("{id}/DeleteTesterToProject")]
        [AuthorizationFilter(Rol.administrator)]

        public IActionResult DeleteTester(Guid id, Project project)
        {
            User tester = new User();
            tester.Id = id;
            try
            {
                testerLogic.DeleteTesterInProject(project, tester);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Project not found or Tester not found");
            }


        }

    }
}