using System.Collections.Generic;
using System;
using Domain;

namespace OBLDA2.Models
{
    public class ProjectOutModel
    {
        public Guid Id { get; set; }
        public List<UserEntryModel> Users { get; set; }
        public List<BugEntryOutModel> Bugs { get; set; }
        public List<TaskEntryOutModel> Task { get; set; }

        public int TotalBugs { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }

        public ProjectOutModel() { }

        public ProjectOutModel(Project project)
        {
            this.Id = project.Id;
            this.Price = project.Price;
            this.Duration = project.Duration;
            this.TotalBugs = project.TotalBugs;
            this.Name = project.Name;

            this.Users = new List<UserEntryModel>();

            if(project.Users!=null)
                foreach (User user in project.Users)
                {
                    this.Users.Add(new UserEntryModel(user));
                }

            this.Bugs = new List<BugEntryOutModel>();
            this.Task = new List<TaskEntryOutModel>();

            if(project.Bugs != null)
                foreach (Bug bug in project.Bugs)
                {
                    this.Bugs.Add(new BugEntryOutModel(bug));
                }

            if(project.Tasks != null)
                foreach (Task task in project.Tasks)
                {
                    this.Task.Add(TaskEntryOutModel.ToModel(task));
                }
        }

    }
}
