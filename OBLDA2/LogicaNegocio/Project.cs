using System.Collections.Generic;
using System;

namespace Domain
{
    public class Project
    {

        public List<User> testers;
        public List<User> desarrolladores;
        public List<Bug> incidentes;

        public string Name { get; set; }

        public Project(string name)
        {
            this.Name = name;
            this.testers = new List<User>();
            this.desarrolladores = new List<User>();
            this.incidentes = new List<Bug>();
        }

    }
}
