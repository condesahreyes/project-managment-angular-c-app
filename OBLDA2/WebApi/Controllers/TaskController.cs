using Microsoft.AspNetCore.Mvc;
using BusinessLogicInterface;
using OBLDA2.Controllers;
using OBLDA2.Models;
using Domain;
using System;

namespace WebApi.Controllers
{
    [Route("penguin/task")]
    public class TaskController : ApiBaseController
    {
        private readonly ITaskLogic taskLogic;

        public TaskController(ITaskLogic taskLogic)
        {
            this.taskLogic = taskLogic;
        }

        [HttpPost]
        public IActionResult CreateTask(TaskEntryOutModel taskModel)
        {
            Task task = taskLogic.Create(taskModel.ToEntity());
            return Ok(TaskEntryOutModel.ToModel(task));
        }
    }
}