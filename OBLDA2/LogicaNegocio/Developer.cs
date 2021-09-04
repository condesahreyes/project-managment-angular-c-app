using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Developer : User
    {

        public Developer(string name, string lastName, string userName, string password, string email/*, string rol*/) :
           base(name, lastName, userName, password, email)
        {

        }
    }
}
