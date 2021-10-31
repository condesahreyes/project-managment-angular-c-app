using System.Text.RegularExpressions;
using System.Collections.Generic;
using Exceptions;
using System;

namespace Domain
{
    public class User
    {
        private const string invalidName = "You must entry a valid name";
        private const string invalidLastName = "You must entry a valid last name";
        private const string invalidUserName = "You must entry a valid username";
        private const string invalidPassword = "You must entry a valid password";
        private const string invalidPrice = "You must entry a valid price";
        private const string invalidEmail = "You must entry a valid email";

        public Guid Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public int Price { get; set; }

        public Rol Rol { get; set; }
        public List<Project> Projects { get; set; }

        public User(string name, string lastName, string userName, string password, 
            string email, Rol rol, int price)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.Rol = rol;
            this.Token = "";
            this.Price = price;
        }

        public User(){}

        public static void IsValidUser(User user)
        {
            ValidateName(user.Name);
            ValidateLastName(user.LastName);
            ValidateUserName(user.UserName);
            ValidatePassword(user.Password);
            ValidateEmail(user.Email);
            ValidatePrice(user.Price);
        }

        private static void ValidateName(string name)
        {
            if (name.Length < 1)
                throw new InvalidDataObjException(invalidName);
        }

        private static void ValidateLastName(string lastName)
        {
            if (lastName.Length < 1)
                throw new InvalidDataObjException(invalidLastName);
        }

        private static void ValidateUserName(string userName)
        {
            if (userName.Length < 1)
                throw new InvalidDataObjException(invalidUserName);
        }

        private static void ValidatePassword(string password)
        {
            if (password.Length < 1)
                throw new InvalidDataObjException(invalidPassword);
        }

        private static void ValidateEmail(string email)
        {
            Regex re = new Regex(@"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                 RegexOptions.IgnoreCase);
            if (!re.IsMatch(email))
            {
                throw new InvalidDataObjException(invalidEmail);
            }
        }

        private static void ValidatePrice(int price)
        {
            if (price < 0 || price > 9999)
                throw new InvalidDataObjException(invalidPassword);
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