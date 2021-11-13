using System;
using System.Diagnostics.CodeAnalysis;
using Domain;

namespace OBLDA2.Models
{
    [ExcludeFromCodeCoverage]
    public class ProjectEntryModel
    {
        public string Name { get; set; }

        public ProjectEntryModel() { }

        public ProjectEntryModel(Project project) {
            this.Name = project.Name;
        }

        public Project ToEntity() => new Project
        {
            Id = Guid.NewGuid(),
            Name = this.Name
        };
    }
}
