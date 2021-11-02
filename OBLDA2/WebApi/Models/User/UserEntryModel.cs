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
        public int Price { get; set; }

        public UserEntryModel(User user)
        {
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.Password = user.Password;
            this.Email = user.Email;
            this.Rol = user.Rol.Name;
            this.Price = user.Price;
        }
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
                Rol = new Rol(this.Rol),
                Price = this.Price
            };
        }

    }
}
