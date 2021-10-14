
using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class TaskLogic : ITaskLogic
    {
        private ITaskRepository taskRepository;

        public TaskLogic(ITaskRepository taskRepository)
        {
            this.taskRepository = taskRepository;
        }

        public Task CreateTask(Task task)
        {
            throw new System.NotImplementedException();
        }
    }
}
