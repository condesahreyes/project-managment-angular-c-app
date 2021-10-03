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
        private readonly ITesterLogic testerLogic;

        public TesterController(ITesterLogic testerLogic)
        {
            this.testerLogic = testerLogic;
        }

        [HttpGet]
        public IActionResult GetAllBugs([FromBody] Guid id)
        {
            User user = new User();
            user.Id = id;

            IEnumerable<Bug> bugs = this.testerLogic.GetAllBugs(user);

            return (StatusCode((int)HttpStatusCode.OK, bugs));

        }

        [HttpPost]
        public IActionResult AssignTester([FromBody] ProjectEntryModel project, [FromBody] UserEntryModel tester)
        {
            try
            {
                testerLogic.AssignTesterToProject(project.ToEntity(), tester.ToEntity());
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Project not found or Tester not found");
            }

        }

        [HttpDelete]
        public IActionResult DeleteTester(Project project, User tester)
        {
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