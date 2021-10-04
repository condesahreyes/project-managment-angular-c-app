using Domain;

namespace OBLDA2.Models
{
    public class ImportBugModel
    {
        
        public string FileAddress { get; set; }
        public ImportBugModel() { }

        public ImportBugModel(string fileAddress)
        {
            this.FileAddress = fileAddress;
        }
    }
}
