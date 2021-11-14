using System;
using System.Diagnostics.CodeAnalysis;
using Domain;

namespace OBLDA2.Models
{
    [ExcludeFromCodeCoverage]
    public class ProjectReportModel
    {
        public Guid Id { get; set; }

        public int TotalBugs { get; set; }

        public string Name { get; set; }

        public ProjectReportModel() { }

        public ProjectReportModel(Project project)
        {
            this.Id = project.Id;
            this.TotalBugs = project.TotalBugs;
            this.Name = project.Name;
        }

    }
}
