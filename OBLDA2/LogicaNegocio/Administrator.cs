using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Administrator : User
    {

        public Administrator(string name, string lastName, string userName, string password, string email/*, string rol*/):
            base(name,lastName, userName, password,email)
        {

        }
    }
}
