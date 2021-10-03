using System;
using Domain;

namespace OBLDA2.Models
{
    public class ProjectEntryModel
    {
        public string Name { get; set; }

        public ProjectEntryModel() { }

        public ProjectEntryModel(Project project) {
            this.Name = project.Name;
        }

        public Project ToEntity() => new Project
        {
            Id = new Guid(),
            Name = this.Name
        };
    }
}
