using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using WebApi.Filters;
using OBLDA2.Models;
using Domain;
using System;

namespace WebApi.Controllers
{
    [Route("penguin/tasks")]
    public class TaskController : ApiBaseController
    {
        private readonly ITaskLogic taskLogic;

        public TaskController(ITaskLogic taskLogic)
        {
            this.taskLogic = taskLogic;
        }

        [HttpPost]
        [AuthorizationFilter(Autorization.AllAutorization)]
        public IActionResult CreateTask(TaskEntryOutModel taskModel)
        {
            Task task = taskLogic.Create(taskModel.ToEntity());
            return Ok(TaskEntryOutModel.ToModel(task));
        }

        [HttpGet]
        [AuthorizationFilter(Autorization.Administrator)]
        public IActionResult GetAllTask()
        {
            List<Task> tasks = taskLogic.GetAll();
            return Ok(TaskEntryOutModel.ToListModel(tasks));
        }

        [HttpGet("{idProject}")]
        [AuthorizationFilter(Autorization.AllAutorization)]
        public IActionResult GetAllTaskByProject(Guid idProject)
        {
            List<Task> tasks = taskLogic.GetAllByProject(idProject);
            return Ok(TaskEntryOutModel.ToListModel(tasks));
        }
    }
}