using System.Collections.Generic;
using Exceptions;
using System;

namespace Domain
{
    public class Project
    {
        private const string invalidName = "You must entry a valid name";

        public Guid Id { get; set; }
        public List<User> Users { get; set; }
        public List<Bug> Bugs { get; set; }
        public List<Task> Tasks { get; set; }
        public int TotalBugs { get; set; }

        public string Name { get; set; }

        public Project(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Users = new List<User>();
            this.Bugs = new List<Bug>();
            this.Tasks = new List<Task>();
            this.TotalBugs = 0;
        }

        public Project() { }

        public static void ValidateName(string name)
        {
            if (name.Length < 1)
                throw new InvalidDataObjException(invalidName);
        }

    }
}
