

namespace OBLDA2.Models
{
    public class BugImportModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Domain { get; set; }
        public string Version { get; set; }
        public string State { get; set; }
        public int Duration { get; set; }
        public string Project { get; set; }

        public BugImportModel() { }

       
    }
}
