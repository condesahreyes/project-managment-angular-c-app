using System;

namespace Domain
{
    public class Bug
    {

        public Project Project { get; set; }
        public User SolvedBy { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }
        public string State { get; set; }

        public Bug(Project project, int id, string name, 
            string domain, string version, string state)
        {
            this.Project = project;
            this.Id = id;
            this.Name = name;
            this.Domain = domain;
            this.Version = version;
            this.State = state;
        }

        public static void AreCorrectData(Bug oneBug)
        {
            IsValidState(oneBug.State);
            IsValidId(oneBug.Id);
            IsValidName(oneBug.Name);
            IsValidDomain(oneBug.Domain);
            IsValidVersion(oneBug.Version);
        }

        private static void IsValidState(string state)
        {
            for (int i = 0; i < StatesBug.all.Length; i++)
            {
                if (StatesBug.all[i] == state.ToLower())
                {
                    return;
                }
            }

            throw new Exception("");
        }

        private static void IsValidId(int oneId)
        {
            if (!(oneId <= 9999 && oneId >= 0))
                throw new Exception("");
        }

        private static void IsValidName(string oneName)
        {
            int nameLength = oneName.Length;

            if (!(nameLength <= 60 && nameLength > 0))
                throw new Exception("");
        }

        private static void IsValidDomain(string oneDomain)
        {
            int domainLength = oneDomain.Length;

            if (!(domainLength <= 150 && domainLength > 0))
                throw new Exception("");
        }

        private static void IsValidVersion(string oneVersion)
        {
            int versionLength = oneVersion.Length;

            if (!(versionLength <= 10 && versionLength > 0))
                throw new Exception("");
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
