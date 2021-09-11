using System;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string Password { get; }
        public string Email { get; }

        public Rol Rol { get; }

        public User(string name, string lastName, string userName, string password, string email, Rol rol)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.Rol = rol;
        }

        public override bool Equals(Object obj){
            var result = false;

            if (obj is User user)
            {
                result = this.Id == user.Id && this.UserName == user.UserName;
            }

            return result;
        }
    }
}
