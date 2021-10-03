using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Linq;
using System.Net;
using Domain;
using System;

namespace WebApi.Controllers
{
    [Route("Penguin/projects")]
    public class ProjectController : ApiBaseController
    {
        private readonly IProjectLogic projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            this.projectLogic = projectLogic;
        }

        [HttpPost]
        public IActionResult AddProject([FromBody] ProjectEntryModel projectDTO)
        {
            Project project = this.projectLogic.Create(projectDTO.ToEntity());
            ProjectOutModel projectAdded = new ProjectOutModel(project);

            return (StatusCode((int)HttpStatusCode.Created, projectAdded));
        }

        [HttpGet]
        public IActionResult GetAllProjects()
        {
            IEnumerable<Project> projects = this.projectLogic.GetAll();
            IEnumerable<ProjectOutModel> projectsOut = projects.Select(p => new ProjectOutModel(p));
            return Ok(projectsOut);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid projectId)
        {
            Project projectToReturn = this.projectLogic.Get(projectId);

            if (projectToReturn != null)
            {
                return Ok(new ProjectOutModel(projectToReturn));
            }
            else
            {
                return NotFound("Project not found with id: " + projectId);
            }
        }

        [HttpGet]
        public IActionResult GetAllBugsByProject(Guid projectId)
        {
            Project project = new Project();
            project.Id = projectId;

            IEnumerable<Bug> bugs = this.projectLogic.GetAllBugByProject(project);
            return Ok(bugs);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                projectLogic.Delete(id);
                return Ok();
            }
            catch (Exception)
            {
                return NotFound("Project not found with id: " + id);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProject([FromRoute] Guid id, [FromBody] ProjectEntryModel projectDTO)
        {
            Project projectUpdated = this.projectLogic.Update(id, projectDTO.ToEntity());
            ProjectOutModel projectAdded = new ProjectOutModel(projectUpdated);

            return (StatusCode((int)HttpStatusCode.Created, projectAdded));
        }

    }
}