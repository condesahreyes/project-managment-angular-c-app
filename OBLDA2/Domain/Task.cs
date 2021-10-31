using Exceptions;
using System;

namespace Domain
{
    public class Task
    {
        private const string invalidName = "You must entry a valid name";
        private const string invalidCost = "You must entry a valid cost. Beetween 0 and 99999999";
        private const string invalidDuration = "You must entry a valid duration. Beetween 0 and 100";

        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Price { get; set; }
        public int Duration { get; set; }

        public Project Project { get; set; }

        public Task(Project project, string name, int price, int duration)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Price = price;
            this.Duration = duration;
            this.Project = project;
        }

        public Task() { }

        public static void ValidateName(string name)
        {
            if (name.Length < 1)
                throw new InvalidDataObjException(invalidName);
        }

        public static void ValidateCost(int cost)
        {
            if (cost < 0 || cost > 99999999)
                throw new InvalidDataObjException(invalidCost);
        }

        public static void ValidateDuration(double duration)
        {
            if (duration < 0 || duration > 9999)
                throw new InvalidDataObjException(invalidDuration);
        }

        public override bool Equals(Object obj)
        {
            if(obj is Task task)
            {
                return this.Name == task.Name && this.Price == task.Price 
                    && this.Duration == task.Duration;
            }

            return false;
        }

    }
}
