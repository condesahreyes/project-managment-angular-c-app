using System;

namespace Domain
{
    public class Rol
    {
        public static string administrator = "Administrador";
        public static string developer = "Desarrollador";
        public static string tester = "Tester";

        public Guid Id { get; set; }
        public string Name { get; set; }

        public Rol(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Rol() { }
    }
}
