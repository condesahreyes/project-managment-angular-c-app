using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class State
    {
        public readonly static string active = "activo";
        public readonly static string done = "resuelto";
        public readonly static string[] all = { active, done };
        public Guid Id { get; set; }
        public string Name { get; set; }

        public State(string name)
        {
            this.Id = new Guid();
            this.Name = name;
        }
    }
}
