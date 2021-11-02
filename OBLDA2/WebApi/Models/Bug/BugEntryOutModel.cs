using System;
using System.Collections.Generic;
using Domain;

namespace OBLDA2.Models
{
    public class BugEntryOutModel
    {
        public int Id { get; set; }
        public int Duration { get; set; }
        public string Project { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }
        public string State { get; set; }
        public string CreatedBy { get; set; }

        public BugEntryOutModel() { }

        public BugEntryOutModel(Bug bug)
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

        public static List<BugEntryOutModel> ListBugs(List<Bug> bugs)
        {
            List<BugEntryOutModel> outModel = new List<BugEntryOutModel>();

            foreach (Bug bug in bugs)
            {
                outModel.Add(new BugEntryOutModel(bug));
            }

            return outModel;
        }
    }
}
