using System;

namespace Domain
{
    public class Rol
    {
        public const string administrator = "Administrador";
        public const string developer = "Desarrollador";
        public const string tester = "Tester";

        public Guid Id { get; set; }
        public string Name { get; set; }

        public Rol(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

    }
}
