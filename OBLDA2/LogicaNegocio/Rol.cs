using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Rol
    {
        Guid Id { get; set; }
        String Name { get; set; }

        public Rol(string name)
        {
            this.Name = name;
        }
    }
}
