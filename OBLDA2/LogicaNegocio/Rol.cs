using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Rol
    {
        Guid Id { get; set; }
        public String Name { get; set; }

        public Rol(Guid id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
    }
}
