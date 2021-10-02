using System;

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
            this.Name = name;
            this.Id = new Guid();
        }

        public State() { }
    }
}
