using System.Collections.Generic;
using Domain;
using System;

namespace BusinessLogicInterface
{
    public interface ITaskLogic
    {
        Task CreateTask(Task task);
    }
}
