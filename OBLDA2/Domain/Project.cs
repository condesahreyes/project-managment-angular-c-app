using System.Collections.Generic;
using System;


namespace Domain
{
    public class Project
    {
        public Guid Id { get; set; }
        public List<User> Users { get; set; }
        public List<Bug> Bugs { get; set; }
        public int TotalBugs { get; set; }

        public string Name { get; set; }

        public Project(string name)
        {
            this.Id = new Guid();
            this.Name = name;
            this.Users = new List<User>();
            this.Bugs = new List<Bug>();
            this.TotalBugs = 0;
        }

        public Project() { }

        public static void ValidateName(string name)
        {
            if (name.Length < 1)
                throw new Exception();
        }

    }
}
