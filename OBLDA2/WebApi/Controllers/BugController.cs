using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Net;
using System.Linq;

namespace WebApi.Controllers
{

    [Route("Penguin/bugs")]
    public class BugController : ApiBaseController
    {
        private readonly IBugLogic bugLogic;

        public BugController(IBugLogic bugLogic)
        {
            this.bugLogic = bugLogic;
        }
        [HttpPost]
        public IActionResult AddBug([FromBody] BugEntryOutModel bugDTO)
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
            //IEnumerable<BugEntryOutModel> bugsOut = bugs.Select(b => BugEntryOutModel.BugEntryOutModel2(b));


            return Ok(bugsOut);
        }

        [HttpGet("{id}")]
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
        public IActionResult Delete(int id)
        {
            try
            {
                bugLogic.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Bug not found with id: " + id);
            }


        }

        [HttpPut("{id}")]
        public IActionResult UpdateABug([FromRoute] int id, [FromBody] BugUpdateModel bugDTO)
        {
            Bug bugUpdated = this.bugLogic.Update(id, bugDTO.ToEntity(id));
            BugEntryOutModel bugUpdateOut = new BugEntryOutModel(bugUpdated);

            return NoContent();

        }

        [HttpPut("{id}", Name = "token")]
        public IActionResult UpdateStateBug([FromBody] BugEntryOutModel bugDto)
        {
            Bug bugReturn = this.bugLogic.UpdateStateToActiveBug(bugDto.Id);
            return NoContent();
        }

        [HttpPut("{id}", Name = "token")]
        public IActionResult UpdateToDoneBug([FromBody] BugEntryOutModel bugDto)
        {
            Bug bugReturn = this.bugLogic.UpdateStateToDoneBug(bugDto.Id);
            return NoContent();
        }
    }
}