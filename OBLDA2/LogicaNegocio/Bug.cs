using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Bug
    {

        public Project Project { get; }
        public int Id { get; }
        public string Name { get; }
        public string Domain { get; }
        public string Version { get;}
        public string State { get;}

        public Bug(Project project, int id, string name, string domain, string version, string state)
        {
            this.Project = project;
            this.Id = id;
            this.Name = name;
            this.Domain = domain;
            this.Version = version;
            this.State = state;
        }
        public override bool Equals(Object obj)
        {
            var result = false;

            if (obj is Bug bug)
            {
                result = (this.Id == bug.Id);
            }

            return result;
        }
    }
}
