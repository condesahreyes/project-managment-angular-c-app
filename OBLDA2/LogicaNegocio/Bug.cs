using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Bug
    {

        public Project Project { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }
        public string State { get; set; }

        public Bug(Project project, int id, string name, string domain, string version, string state)
        {
            this.Project = project;
            this.Id = id;
            this.Name = name;
            this.Domain = domain;
            this.Version = version;
            this.State = state;
        }
        /*
        public override bool Equals(object obj)
        {
            var result = false;

            if (obj is Bug bug)
            {
                result = this.Project.Name == bug.Project.Name ||
                    this.Id == bug.Id || this.Name == bug.Name;
            }

            return result;
        }
        */
    }
}
