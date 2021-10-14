using BusinessLogicInterface;
using DataAccessInterface;
using Domain;

namespace BusinessLogic
{
    public class TaskLogic : ITaskLogic
    {
        private ITaskRepository taskRepository;
        private IProjectLogic projectLogic;

        public TaskLogic(ITaskRepository taskRepository, IProjectLogic projectLogic)
        {
            this.taskRepository = taskRepository;
            this.projectLogic = projectLogic;
        }

        public Task CreateTask(Task task)
        {
            IsValidTask(task);

            return taskRepository.Create(task);
        }

        private void IsValidTask(Task task)
        {
            Task.ValidateName(task.Name);
            Task.ValidateDuration(task.Duration);
            Task.ValidateCost(task.Cost);

            projectLogic.ExistProjectWithName(task.Project);
        }
    }
}
