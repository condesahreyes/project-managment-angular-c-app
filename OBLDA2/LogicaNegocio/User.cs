using System.Text.RegularExpressions;
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

        public User(Guid id, string name, string lastName, string userName, string password, string email, Rol rol)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.Rol = rol;
        }

        public static void IsValidUser(User user)
        {
            ValidateName(user.Name);
            ValidateLastName(user.LastName);
            ValidateUserName(user.UserName);
            ValidatePassword(user.Password);
            ValidateEmail(user.Email);
        }

        private static void ValidateName(string name)
        {
            if (name.Length < 1)
                throw new Exception();
        }

        private static void ValidateLastName(string lastName)
        {
            if (lastName.Length < 1)
                throw new Exception();
        }

        private static void ValidateUserName(string userName)
        {
            if (userName.Length < 1)
                throw new Exception();
        }

        private static void ValidatePassword(string password)
        {
            if (password.Length < 1)
                throw new Exception();
        }

        private static void ValidateEmail(string email)
        {
            Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                 RegexOptions.IgnoreCase);
            if (!re.IsMatch(email))
            {
                throw new Exception();
            }
        }
        
        public override bool Equals(Object obj)
        {
            var result = false;

            if (obj is User user)
            {
                result = this.Id == user.Id && this.UserName == user.UserName;

            }
            return result;
        } 
        
    }
}