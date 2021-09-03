using System.Collections.Generic;
using System;

namespace Domain
{
    public class Project
    {
        private string nombre;

        public List<User> testers;
        public List<User> desarrolladores;
        public List<Bug> incidentes;

        public string Nombre { get; }

        public Project(string nommbre)
        {
            this.nombre = nombre;
            this.testers = new List<User>();
            this.desarrolladores = new List<User>();
            this.incidentes = new List<Bug>();
        }

    }
}
