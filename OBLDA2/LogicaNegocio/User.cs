using System;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name{ get;}
        public string LastName{ get;}
        public string UserName{ get;}
        public string Password{ get;}
        public string Email{ get; }

        public Rol Rol{ get; }

        public User(Guid id, string name, string lastName, string userName, string password, string email, Rol rol)
        {
            this.Id = id;
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.Rol = rol;
        }

    }
}
