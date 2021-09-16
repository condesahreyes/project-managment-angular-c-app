using System.Collections.Generic;
using System;

namespace Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public List<User> testers { get; set; }
        public List<User> desarrolladores { get; set; }
        public List<Bug> incidentes { get; set; }
        public int totalBugs { get; set; }

        public string Name { get; set; }

        public Project(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.testers = new List<User>();
            this.desarrolladores = new List<User>();
            this.incidentes = new List<Bug>();
            this.totalBugs = 0;
        }

    }
}
