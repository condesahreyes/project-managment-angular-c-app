﻿using System.Collections.Generic;
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
            List<Project> projects = this.projectLogic.GetAll();

            List<ProjectOutModel> projectsOut = new List<ProjectOutModel>();

            foreach (Project project in projects)
            {
                projectsOut.Add(new ProjectOutModel(project));
            }

            return Ok(projectsOut);
        }

        [HttpGet("{projectId}")]
        [AuthorizationFilter(Autorization.AllAutorization)]
        public IActionResult GetById(Guid projectId)
        {
            Project projectToReturn = this.projectLogic.Get(projectId);

             return Ok(new ProjectOutModel(projectToReturn));
        }

        [HttpGet("{projectId}/bugs")]
        [AuthorizationFilter(Autorization.AllAutorization)]
        public IActionResult GetAllBugsByProject(Guid projectId)
        {
            List<Bug> bugs = this.projectLogic.GetAllBugByProject(projectId);
            return Ok(BugEntryOutModel.ListBugs(bugs));
        }

        [HttpDelete("{id}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult Delete(Guid id)
        {
            projectLogic.Delete(id);
            return NoContent();
        }

        [HttpPut("{id}")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult UpdateProject(Guid id, ProjectEntryModel projectDTO)
        {
            this.projectLogic.Update(id, projectDTO.ToEntity());
            return NoContent();
        }

        [HttpGet("{projectId}/users")]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult GetAllUsersByProject(Guid projectId)
        {
            List<User> users = this.projectLogic.GetAllUsersInOneProject(projectId);
            return Ok(UserOutModel.ListUser(users));
        }

    }
}