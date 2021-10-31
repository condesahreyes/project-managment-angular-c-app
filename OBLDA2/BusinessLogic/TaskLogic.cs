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

        public Task Create(Task task)
        {
            IsValidTask(ref task);

            return taskRepository.Create(task);
        }

        private void IsValidTask(ref Task task)
        {
            Task.ValidateName(task.Name);
            Task.ValidateDuration(task.Duration);
            Task.ValidateCost(task.Price);

            task.Project = projectLogic.ExistProjectWithName(task.Project);
        }
    }
}
