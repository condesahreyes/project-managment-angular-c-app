using Exceptions;
using System;

namespace Domain
{
    public class Bug
    {
        private const string invalidId = "You must entry a valid id";
        private const string invalidName = "You must entry a valid name";
        private const string invalidDomain = "You must entry a valid domain";
        private const string invalidVersion = "You must entry a valid version";
        private const string invalidDuration = "You must entry a valid duration. Beetween 0 and 100";

        public int Id { get; set; }

        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }

        public int Duration { get; set; }
        public Project Project { get; set; }
        public User SolvedBy { get; set; }
        public State State { get; set; }

        public Bug(Project project, int id, string name, 
            string domain, string version, State state, int duration)
        {
            this.Project = project;
            this.Id = id;
            this.Name = name;
            this.Domain = domain;
            this.Version = version;
            this.State = state;
            this.Duration = duration;
        }

        public Bug() { }

        public static void AreCorrectData(Bug oneBug)
        {
            IsValidId(oneBug.Id);
            IsValidName(oneBug.Name);
            IsValidDomain(oneBug.Domain);
            IsValidVersion(oneBug.Version);
            ValidateDuration(oneBug.Duration);
        }

        private static void IsValidId(int oneId)
        {
            if (!(oneId <= 9999 && oneId >= 0))
                throw new InvalidDataObjException(invalidId);
        }

        private static void IsValidName(string oneName)
        {
            int nameLength = oneName.Length;

            if (!(nameLength <= 60 && nameLength > 0))
                throw new InvalidDataObjException(invalidName);
        }

        private static void IsValidDomain(string oneDomain)
        {
            int domainLength = oneDomain.Length;

            if (!(domainLength <= 150 && domainLength > 0))
                throw new InvalidDataObjException(invalidDomain);
        }

        private static void IsValidVersion(string oneVersion)
        {
            int versionLength = oneVersion.Length;

            if (!(versionLength <= 10 && versionLength > 0))
                throw new InvalidDataObjException(invalidVersion);
        }

        public static void ValidateDuration(double duration)
        {
            if (duration < 0 || duration > 9999)
                throw new InvalidDataObjException(invalidDuration);
        }

        public override bool Equals(Object obj)
        {
            var result = false;

            if (obj is Bug bug)
            {
                result = (this.Id == bug.Id);
            }

            return result;
        }

    }
}
