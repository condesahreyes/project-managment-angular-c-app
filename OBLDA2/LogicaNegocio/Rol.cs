﻿using System;

namespace Domain
{
    public class Rol
    {
        Guid Id { get; set; }
        public String Name { get; set; }

        public Rol(Guid id, string name)
        {
<<<<<<< HEAD
            //this.Id = id;
=======
            this.Id = id;
>>>>>>> administratorLogicTest
            this.Name = name;
        }

      /*  public static void IsValidRolName(string rolName)
        {
            if (rolName.Length < 1)
            {
                throw new Exception("You can't enter rol name empty ");
            }

            if (!rolName.Equals("Administrator") || !rolName.Equals("Tester") || !rolName.Equals("Developer"))
            {
                throw new Exception("The unique roles suported are: Administrato - Tester - Developer");
            }
        }*/
    }
}
