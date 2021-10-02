using Domain;

namespace OBLDA2.Models
{
    public class BugUpdateModel
    {
        public string Project { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }
        public string State { get; set; }

        public BugUpdateModel() { }

        public Bug ToEntity(int id) => new Bug
        {
            Id = id,
            State = new State(this.State),
            Project = new Project(this.Project),
            Version = this.Version,
            Domain = this.Domain,
            Name = this.Name
        };

    }
}
