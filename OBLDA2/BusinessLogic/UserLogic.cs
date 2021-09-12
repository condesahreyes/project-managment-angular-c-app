using System.Text.RegularExpressions;
using System;

namespace BusinessLogic
{
    public class UserLogic
    {
        public static string ValidateUser(string name)
        {
            if (name.Length < 1)
                throw new Exception();

            return name;
        }

        public static string ValidateSurname(string surname)
        {
            if (surname.Length < 1)
                throw new Exception();

            return surname;
        }

        public static string ValidateUserName(string userName)
        {
            if (userName.Length < 1)
                throw new Exception();

            return userName;
        }

        public bool mailValidation(string mailCorrecto)
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

        public bool passValidation(string pass)
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
    }
}
