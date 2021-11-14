using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using WebApi.Filters;
using OBLDA2.Models;
using System.Net;
using System;
using Domain;

namespace WebApi.Controllers
{
    [Route("penguin/developers")]
    public class DeveloperController : ApiBaseController
    {
        private readonly IDeveloperLogic developerLogic;

        public DeveloperController(IDeveloperLogic developerLogic)
        {
            this.developerLogic = developerLogic;
        }

        [HttpGet("{idDeveloper}/bugs")]
        [AuthorizationFilter(Autorization.DeveloperAndAdmin)]
        public IActionResult GetAllBugsDeveloper(Guid idDeveloper)
        {
            List<Bug> bugs = this.developerLogic.GetAllBugs(idDeveloper);
            IEnumerable<BugEntryOutModel> bugsModel = BugEntryOutModel.ListBugs(bugs);

            return (StatusCode((int)HttpStatusCode.OK, bugsModel));
        }
        
        [HttpPost("{idDeveloper}/project/{idProject}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult AssignDeveloperToProject(Guid idProject, Guid idDeveloper)
        {
            developerLogic.AssignDeveloperToProject(idProject, idDeveloper);
            return NoContent();
        }

        [HttpDelete("{idDeveloper}/project/{idProject}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult DeleteDeveloperToProject(Guid idDeveloper, Guid idProject)
        {
            developerLogic.DeleteDeveloperInProject(idProject, idDeveloper);
            return NoContent();
        }

        [HttpGet("{idDeveloper}/countBugs")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult GetCountBugsResolvedByDeveloper(Guid idDeveloper)
        {
            int countBugsResolved = this.developerLogic.CountBugDoneByDeveloper(idDeveloper);
            return (StatusCode((int)HttpStatusCode.OK, countBugsResolved));
        }

        [HttpPut("{developerId}/bugState")]
        [AuthorizationFilter(Autorization.AllAutorization)]
        public IActionResult UpdateStateBug(Guid developerId, BugUpdateStateModel updateState)
        {
            this.developerLogic.UpdateState(updateState.BugId, 
                updateState.State, developerId);

            return NoContent();
        }

        [HttpGet]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult GetAll()
        {
            List<User> users = this.developerLogic.GetAll();
            IEnumerable<UserOutModel> usersOut = UserOutModel.ListUser(users);

            return Ok(usersOut);
        }

        [HttpGet("{idDeveloper}/projects")]
        [AuthorizationFilter(Autorization.Developer)]
        public IActionResult GetAllProjectsDeveloper(Guid idDeveloper)
        {
            List<Project> projects = this.developerLogic.GetAllProjects(idDeveloper);

            return (StatusCode((int)HttpStatusCode.OK, projects));
        }

        [HttpGet("{idDeveloper}/tasks")]
        [AuthorizationFilter(Autorization.Developer)]
        public IActionResult GetAllTask(Guid idDeveloper)
        {
            List<Task> tasks = developerLogic.GetAllTask(idDeveloper);
            return Ok(TaskEntryOutModel.ToListModel(tasks));
        }
    }
}