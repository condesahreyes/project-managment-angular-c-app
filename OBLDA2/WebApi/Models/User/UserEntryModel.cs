using Domain;

namespace OBLDA2.Models
{
    public class UserEntryModel
    {
        public string Rol { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public UserEntryModel() { }

        public User ToEntity()
        {
            return new User
            {
                Name = this.Name,
                LastName = this.LastName,
                UserName = this.UserName,
                Password = this.Password,
                Email = this.Email,
                Rol = new Rol(this.Rol)
            };
        }

    }
}
