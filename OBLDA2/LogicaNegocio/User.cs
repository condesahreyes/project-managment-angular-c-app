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

        public static bool CorrectEmailAndPassword(string email, string pass)
        {
            bool correctMail = ValidateEmail(email);
            bool correctPass = ValidatePass(pass);
            if (correctMail && correctPass)
            {
                return true;
            }
            else
            {
                return false;
            }
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

        public static bool CorrectData(User user) //esto habria que ver si esta bien asi! o trater de hacer un metodo para cada Set de cada
                                                  //atributo
        {
            bool isValid = false;

            if (user.Name.Length < 1 || user.LastName.Length < 1 || user.UserName.Length < 1)
            {
                throw new Exception("You can enter empty data");
            }

            if (CorrectEmailAndPassword(user.Email, user.Password))
            {
                isValid = true;

            }
            return isValid;

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
