using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Developer : User
    {

        public List<Project> projects;

        public Developer(Guid id, string name, string lastName, string userName, string password, string email) :
           base(id, name, lastName, userName, password, email)
        {
            this.projects = new List<Project>();
        }
    }
}
