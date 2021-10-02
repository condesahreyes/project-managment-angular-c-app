using System;
using Domain;

namespace OBLDA2.Models
{
    public class ProjectEntryModel
    {
        public string Name { get; set; }

        public ProjectEntryModel() { }

        public Project toEntity() => new Project
        {
            Id = new Guid(),
            Name = this.Name
        };
    }
}
