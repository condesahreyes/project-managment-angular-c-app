using System.Text.RegularExpressions;
using System;

namespace BusinessLogic
{
    public class UserLogic
    {
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
        }

        public static string ValidateEmail(string email)
        {
            string character = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            if (!Regex.IsMatch(email, character))
                throw new Exception();

            return email;
        }
    }
}
