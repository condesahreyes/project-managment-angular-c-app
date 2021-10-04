﻿using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using System.Linq;
using System.Net;
using Domain;
using System;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("penguin/projects")]
    public class ProjectController : ApiBaseController
    {
        private readonly IProjectLogic projectLogic;

        public ProjectController(IProjectLogic projectLogic)
        {
            this.projectLogic = projectLogic;
        }

        [HttpPost]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult AddProject(ProjectEntryModel projectDTO)
        {
            Project project = this.projectLogic.Create(projectDTO.ToEntity());

            ProjectOutModel projectAdded = new ProjectOutModel(project);

            return (StatusCode((int)HttpStatusCode.Created, projectAdded));
        }

        [HttpGet]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult GetAllProjects()
        {
            IEnumerable<Project> projects = this.projectLogic.GetAll();

            List<ProjectOutModel> projectsOut = new List<ProjectOutModel>();

            foreach (Project project in projects)
            {
                projectsOut.Add(new ProjectOutModel(project));
            }

            return Ok(projectsOut);
        }

        [HttpGet]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult GetTotalBugsByProjects()
        {
            IEnumerable<Project> projects = this.projectLogic.GetAll();

            List<ProjectReportModel> projectsOut = new List<ProjectReportModel>();

            foreach (Project project in projects)
            {
                projectsOut.Add(new ProjectReportModel(project));
            }

            return Ok(projectsOut);
        }

        [HttpGet("{projectId}")]
        [AuthorizationFilter(Autorization.Administrator)]

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

        [HttpGet("{projectId}/GetAllBugs")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult GetAllBugsByProject(Guid projectId)
        {
            Project project = new Project();
            project.Id = projectId;

            IEnumerable<Bug> bugs = this.projectLogic.GetAllBugByProject(project);
            return Ok(bugs);
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult Delete(Guid id)
        {
            try
            {
                projectLogic.Delete(id);
                return NoContent();
            }
            catch (Exception)
            {
                return NotFound("Project not found with id: " + id);
            }
        }

        [HttpPut("{id}")]
        [AuthorizationFilter(Autorization.Administrator)]

        public IActionResult UpdateProject(Guid id, ProjectEntryModel projectDTO)
        {
            Project projectUpdated = this.projectLogic.Update(id, projectDTO.ToEntity());
            ProjectOutModel projectAdded = new ProjectOutModel(projectUpdated);

            return NoContent();
        }

    }
}