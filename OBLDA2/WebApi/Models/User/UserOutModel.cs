using System.Collections.Generic;
using System.Linq;
using Domain;

namespace OBLDA2.Models
{
    public class UserOutModel
    {
        public string Rol { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IEnumerable<ProjectOutModel> Projects { get; set; }

        public UserOutModel() { }

        public UserOutModel(User user)
        {
            this.Rol = user.Rol.Name;
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.Password = user.Password;
            this.Email = user.Email;

            this.Projects = new List<ProjectOutModel>();
            this.Projects = user.Projects.Select(p => new ProjectOutModel(p));
        }

    }
}
