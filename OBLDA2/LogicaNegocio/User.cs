﻿using System;

namespace Domain
{
    public class User
    {

        public string Name{ get;}
        public string LastName{ get;}
        public string UserName{ get;}
        public string Password{ get;}
        public string Email{ get; }
        //public string Rol{ get; }

        public User(string name, string lastName, string userName, string password, string email/*, string rol*/)
        {
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
           // this.Rol = rol;
        }

    }
}
