using System;
using Domain;

namespace OBLDA2.Models
{
    public class ProjectEntryModel
    {
        public string Name { get; set; }

        public ProjectEntryModel() { }

<<<<<<< HEAD
        public ProjectEntryModel(Project project) {
=======
        public ProjectEntryModel(Project project)
        {
>>>>>>> feature/ProjectController
            this.Name = project.Name;
        }

        public Project ToEntity() => new Project
        {
            Id = new Guid(),
            Name = this.Name
        };
    }
}
