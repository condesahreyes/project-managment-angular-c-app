using System.Collections.Generic;
using System.Linq;
using System;
using Domain;

namespace OBLDA2.Models
{
    public class ProjectOutModel
    {
        public Guid Id { get; set; }
        public IEnumerable<UserOutModel> Users { get; set; }
        public IEnumerable<BugEntryOutModel> Bugs { get; set; }

        public int TotalBugs { get; set; }

        public string Name { get; set; }

        public ProjectOutModel() { }

        public ProjectOutModel(Project project)
        {
            this.Id = project.Id;
            this.Users = project.Users.Select(u => new UserOutModel(u));
            this.Bugs = project.Bugs.Select(b => new BugEntryOutModel(b));
            this.TotalBugs = project.TotalBugs;
            this.Name = project.Name;
        }

    }
}
