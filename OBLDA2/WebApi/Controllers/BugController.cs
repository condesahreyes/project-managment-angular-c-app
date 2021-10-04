using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Net;
using Domain;
using System;
using WebApi.Filters;

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
        [AuthorizationFilter(Rol.administrator)]

        public IActionResult AddBug(BugEntryOutModel bugDTO)
        {
            Bug bug = this.bugLogic.Create(bugDTO.ToEntity());
            BugEntryOutModel bugAdded = new BugEntryOutModel(bug);

            return (StatusCode((int)HttpStatusCode.Created, bugAdded));
        }

        [HttpGet]
        public IActionResult GetAllBugs()
        {
            List<Bug> bugs = this.bugLogic.GetAll();
            List<BugEntryOutModel> bugsOut = new List<BugEntryOutModel>();

            foreach (var bug in bugs)
            {
                bugsOut.Add(new BugEntryOutModel(bug));
            }

            return Ok(bugsOut);
        }

        [HttpGet("{id}")]
        [AuthorizationFilter(Rol.tester)]
        public IActionResult GetById(int bugId)
        {
            Bug bugToReturn = this.bugLogic.Get(bugId);

            if (bugToReturn != null)
            {
                return Ok(new BugEntryOutModel(bugToReturn));
            }
            else
            {
                return NotFound("Bug not found with id: " + bugId);
            }
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter(Rol.administrator + "," + Rol.tester)]

        public IActionResult Delete(int id)
        {
            try
            {
                bugLogic.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Bug not found with id: " + id);
            }
        }

        [HttpPut("{id}")]
        [AuthorizationFilter(Rol.administrator + "," + Rol.tester)]

        public IActionResult UpdateABug(int id, BugUpdateModel bugDTO)
        {
            Bug bugUpdated = this.bugLogic.Update(id, bugDTO.ToEntity(id));
            BugEntryOutModel bugUpdateOut = new BugEntryOutModel(bugUpdated);

            return NoContent();
        }

        [HttpPut("{id}/UpdateState")]
        [AuthorizationFilter(Rol.developer)]
        public IActionResult UpdateStateBug(int id, string state)
        {
            Bug bugReturn = this.bugLogic.UpdateState(id, state);
            return NoContent();
        }
    }
}