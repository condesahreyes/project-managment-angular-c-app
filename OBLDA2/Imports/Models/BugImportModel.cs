using System.Collections.Generic;
using Domain;

namespace OBLDA2.Models
{
    public class BugImportModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }
        public string State { get; set; }
        public int Duration { get; set; }
        public string Project { get; set; }

        public BugImportModel() { }

        public BugImportModel(Bug bug)
        {
            this.Project = bug.Project.Name;
            this.Id = bug.Id;
            this.Name = bug.Name;
            this.Domain = bug.Domain;
            this.Version = bug.Version;
            this.State = bug.State.Name;
            this.Duration = bug.Duration;
        }

        public Bug ToEntity() => new Bug
        {
            Project = new Project(this.Project),
            Id = this.Id,
            Name = this.Name,
            Domain = this.Domain,
            Version = this.Version,
            State = new State(this.State),
            Duration = this.Duration
        };

        public static List<Bug> ListBugs(List<BugImportModel> bugsModel)
        {
            List<Bug> bugs = new List<Bug>();

            foreach (BugImportModel bug in bugsModel)
            {
                bugs.Add(bug.ToEntity());
            }

            return bugs;
        }
    }
}
