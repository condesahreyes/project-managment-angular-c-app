using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Tester : User
    {

        public List<Project> projects;

        public Tester(Guid id, string name, string lastName, string userName, string password, string email) :
           base(id, name, lastName, userName, password, email)
        {
            this.projects = new List<Project>();
        }
    }
}
