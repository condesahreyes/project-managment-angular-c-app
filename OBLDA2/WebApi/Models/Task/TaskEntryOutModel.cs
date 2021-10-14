using Domain;
using System;

namespace OBLDA2.Models
{
    public class TaskEntryOutModel
    {
        public string Name { get; set; }
        public int Cost { get; set; }
        public double Duration { get; set; }
        public string Project { get; set; }

        public TaskEntryOutModel() { }

        public Task ToEntity() => new Task
        {
            Id = Guid.NewGuid(),
            Name = this.Name,
            Cost = this.Cost,
            Duration = this.Duration,
            Project = new Project(this.Project)
        };

        public static TaskEntryOutModel ToModel(Task task) => new TaskEntryOutModel
        {
            Name = task.Name,
            Cost = task.Cost,
            Duration = task.Duration,
            Project = task.Project.Name
        };
    }
}
