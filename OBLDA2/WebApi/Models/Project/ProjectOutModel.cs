using System.Collections.Generic;
using System.Linq;
using System;
using Domain;

namespace OBLDA2.Models
{
    public class ProjectOutModel
    {
        public Guid Id { get; set; }
        public List<UserEntryModel> Users { get; set; }
        public List<BugEntryOutModel> Bugs { get; set; }

        public int TotalBugs { get; set; }

        public string Name { get; set; }

        public ProjectOutModel() { }

        public ProjectOutModel(Project project)
        {
            this.Id = project.Id;


            this.TotalBugs = project.TotalBugs;
            this.Name = project.Name;

            this.Users = new List<UserEntryModel>();

            if(project.Users!=null)
            foreach (User user in project.Users)
            {
                this.Users.Add(new UserEntryModel(user));
            }

            this.Bugs = new List<BugEntryOutModel>();

            if(project.Bugs != null)
            foreach (Bug bug in project.Bugs)
            {
                this.Bugs.Add(new BugEntryOutModel(bug));
            }
        }

    }
}
