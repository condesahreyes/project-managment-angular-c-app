using System.Text.RegularExpressions;
using System;
using Domain;
using DataAccessInterface;

namespace BusinessLogic
{
    public class UserLogic
    {
        // aca no podriamos tener un constructor en el cual instanciemos el repositorio<User>
        // y hacemos el metodo existeUSer para que todos usen el mismo????
        /*
        public static string ValidateName(string name)
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
        }*/

        /*
        public static string DataValidation(User user)
        {
            string validName = ValidateName(user.Name);
            string validSurename = ValidateSurname(user.LastName);
            string validUserName = ValidateUserName(user.UserName);

            string validEmail = ValidateEmail(user.Email);
            string validPassword = Password;
        }
        */

        

        
        
    }
}
