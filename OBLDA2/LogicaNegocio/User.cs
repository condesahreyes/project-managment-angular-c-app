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
            }

            return result;
        }

    }
}
