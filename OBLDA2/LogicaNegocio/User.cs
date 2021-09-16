using System.Text.RegularExpressions;
using System;

namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }
<<<<<<< HEAD
        public string Name { get; }
        public string LastName { get; }
        public string UserName { get; }
        public string Password { get; }
        public string Email { get; }

        public Rol Rol { get; }
=======
        public string Name{ get;}
        public string LastName{ get;}
        public string UserName{ get;}
        public string Password{ get;}
        public string Email{ get; }

        public Rol Rol{ get; }
>>>>>>> administratorLogicTest

        public User(Guid id, string name, string lastName, string userName, string password, string email, Rol rol)
        {
<<<<<<< HEAD
            this.Id = Guid.NewGuid();
=======
            this.Id = id;
>>>>>>> administratorLogicTest
            this.Name = name;
            this.LastName = lastName;
            this.UserName = userName;
            this.Password = password;
            this.Email = email;
            this.Rol = rol;
        }

<<<<<<< HEAD
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

        public override bool Equals(Object obj){
            var result = false;

            if (obj is User user)
            {
                result = this.Id == user.Id && this.UserName == user.UserName;
=======
      
        public static bool ValidateEmail(string mailCorrecto)
        {
            if (mailCorrecto == "")
            {
                throw new Exception("You can enter empty email");
            }

            bool esValido = true;

            if (!mailCorrecto.Contains("@"))
            {
                esValido = false;
            }
            else
            {
                string[] mailArray = mailCorrecto.Split('@');
                if (mailArray.Length != 2)
                {
                    esValido = false;
                }
                else
                {
                    string[] servidor = mailArray[1].Split('.');
                    if (servidor.Length < 2 || servidor.Length > 3)
                    {
                        esValido = false;
                    }
                }
            }

            if (!esValido)
            {
                throw new Exception("Invalid email format");
            }
            else
            {
                return esValido;
            }

        }

        public static bool ValidatePass(string pass)
        {
            if (pass == "")
            {
                throw new Exception("You can enter empty password");
            }
            if (pass.Length > 6 && pass.Length <= 8)
            {
                return true;
            }
            else
            {
                throw new Exception("Invalid password");
            }
        }

        public static bool CorrectData(User user) 
        {
            bool isValid = false;

            if (ValidateEmail(user.Email) && ValidatePass(user.Password) && ValidateName(user.Name) &&
                ValidateLastName(user.LastName) && ValidateUserName(user.UserName))
            {
                isValid = true;

            }
            return isValid;

        }

        private static bool ValidateName(string name)
        {
            if (name.Length < 1)
            {
                throw new Exception("You can't enter name empty ");
            }
            return true;
        }

        private static bool ValidateUserName(string userName)
        {
            if (userName.Length < 1)
            {
                throw new Exception("You can't enter user name empty ");
            }
            return true;
        }

        private static bool ValidateLastName(string lastName)
        {
            if (lastName.Length < 1)
            {
                throw new Exception("You can't enter lastName empty ");
            }
            return true;
        }
        public override bool Equals(Object obj)
        {
            var result = false;

            if (obj is User administrator)
            {
                result = this.Id == administrator.Id && this.Email.Equals(administrator.Email);
>>>>>>> administratorLogicTest
            }

            return result;
        }
<<<<<<< HEAD
=======

>>>>>>> administratorLogicTest
    }
}