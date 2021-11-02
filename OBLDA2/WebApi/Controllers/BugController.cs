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

    [Route("penguin/bugs")]
    public class BugController : ApiBaseController
    {
        private readonly IBugLogic bugLogic;

        public BugController(IBugLogic bugLogic)
        {
            this.bugLogic = bugLogic;
        }

        [HttpPost]
        [AuthorizationFilter(Autorization.AdministratorAndTester)]
        public IActionResult AddBug(BugEntryOutModel bugDTO)
        {
            Bug bug = this.bugLogic.CreateByUser(bugDTO.ToEntity(), Guid.Parse(bugDTO.CreatedBy));
            BugEntryOutModel bugAdded = new BugEntryOutModel(bug);
            bugAdded.CreatedBy = bugDTO.CreatedBy;

            return (StatusCode((int)HttpStatusCode.Created, bugAdded));
        }

        [HttpGet]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult GetAllBugs()
        {
            List<Bug> bugs = this.bugLogic.GetAll();
            List<BugEntryOutModel> bugsOut = BugEntryOutModel.ListBugs(bugs);

            return Ok(bugsOut);
        }


        [HttpGet("byName")]
        [AuthorizationFilter(Autorization.AdministratorAndTester)]
        public IActionResult GetBugsByName(string name)
        {
            List<Bug> bugs = this.bugLogic.GetBugsByName(name);
            List<BugEntryOutModel> bugsOut = BugEntryOutModel.ListBugs(bugs);

            return Ok(bugsOut);
        }

        [HttpGet("byState")]
        [AuthorizationFilter(Autorization.AdministratorAndTester)]
        public IActionResult GetBugsByState(string state)
        {
            List<Bug> bugs = this.bugLogic.GetBugsByState(state);
            List<BugEntryOutModel> bugsOut = BugEntryOutModel.ListBugs(bugs);

            return Ok(bugsOut);
        }

        [HttpGet("byProject")]
        [AuthorizationFilter(Autorization.AdministratorAndTester)]
        public IActionResult GetBugsByProject(string project)
        {
            List<Bug> bugs = this.bugLogic.GetBugsByProject(project);
            List<BugEntryOutModel> bugsOut = BugEntryOutModel.ListBugs(bugs);

            return Ok(bugsOut);
        }

        [HttpGet("{bugId}")]
        [AuthorizationFilter(Autorization.AdministratorAndTester)]
        public IActionResult GetById(int bugId, UserIdModel user)
        {
            Bug bugToReturn = this.bugLogic.Get(bugId, user.UserId);
            return Ok(new BugEntryOutModel(bugToReturn));
        }

        [HttpDelete("{bugId}")]
        [AuthorizationFilter(Autorization.AdministratorAndTester)]
        public IActionResult Delete(int bugId, UserIdModel user)
        {
            bugLogic.Delete(bugId, user.UserId);
            return NoContent();
        }

        [HttpPut("{bugId}")]
        [AuthorizationFilter(Autorization.AdministratorAndTester)]
        public IActionResult UpdateABug(int bugId, BugUpdateModel bugDTO)
        {
            this.bugLogic.Update(bugId, bugDTO.ToEntity(bugId), bugDTO.UserId);
            return NoContent();
        }

    }
}