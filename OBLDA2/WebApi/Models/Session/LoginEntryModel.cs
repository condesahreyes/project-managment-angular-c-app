using System.Diagnostics.CodeAnalysis;

namespace OBLDA2.Models
{
    [ExcludeFromCodeCoverage]
    public class LoginEntryModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
