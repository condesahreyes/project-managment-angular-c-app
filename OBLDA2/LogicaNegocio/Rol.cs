using System;

namespace Domain
{
    public class Rol
    {
        public static string administrator = "Administrador";
        public static string developer = "Desarrollador";
        public static string tester = "Tester";

        Guid Id { get; set; }
        public String Name { get; set; }

        public Rol(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
