using Domain;

namespace OBLDA2.Models
{
    public class BugEntryOutModel
    {
        public string Project { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }
        public string State { get; set; }

        public BugEntryOutModel() { }

        public BugEntryOutModel(Bug bug)
        {
            this.Project = bug.Project.Name;
            this.Id = bug.Id;
            this.Name = bug.Name;
            this.Domain = bug.Domain;
            this.Version = bug.Version;
            this.State = bug.State.Name;
        }

        public Bug ToEntity() => new Bug
        {
            Project = new Project(this.Project),
            Id = this.Id,
            Name = this.Name,
            Domain = this.Domain,
            Version = this.Version,
            State = new State(this.State)
        };
    }
}
