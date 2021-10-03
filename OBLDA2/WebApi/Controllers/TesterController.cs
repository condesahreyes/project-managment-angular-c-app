using BusinessLogicInterface;
using Domain;
using Microsoft.AspNetCore.Mvc;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System;
using System.Collections.Generic;
using System.Net;

namespace WebApi.Controllers
{
    [Route("Penguin/testers")]
    public class TesterController : ApiBaseController
    {
        private ITesterLogic testerLogic;

        public TesterController(ITesterLogic testerLogic)
        {
            this.testerLogic = testerLogic;
        }

        [HttpGet("{id}/GetAllBugs")]
        public IActionResult GetAllBugs(Guid id)
        {
            User user = new User();
            user.Id = id;

            IEnumerable<Bug> bugs = this.testerLogic.GetAllBugs(user);

            return (StatusCode((int)HttpStatusCode.OK, bugs));

        }

        [HttpPost("{id}")]
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

        [HttpDelete("{id}")]
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