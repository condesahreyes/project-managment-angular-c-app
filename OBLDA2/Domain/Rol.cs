using System;

namespace Domain
{
    public class Rol
    {
        public static string administrator = "Administrador";
        public static string developer = "Desarrollador";
        public static string tester = "Tester";

        public Guid Id { get; set; }
        public String Name { get; set; }

        public Rol(string name)
        {
            this.Id = new Guid();
            this.Name = name;
        }

        public Rol() { }
    }
}
