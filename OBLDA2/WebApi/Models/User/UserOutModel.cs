using System.Collections.Generic;
using System;
using Domain;
using System.Diagnostics.CodeAnalysis;

namespace OBLDA2.Models
{
    [ExcludeFromCodeCoverage]
    public class UserOutModel
    {
        public Guid Id { get; set; }
        public int Price { get; set; }
        public string Rol { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<string> Projects { get; set; }

        public UserOutModel() { }

        public UserOutModel(User user)
        {
            this.Rol = user.Rol.Name;
            this.Name = user.Name;
            this.LastName = user.LastName;
            this.UserName = user.UserName;
            this.Password = user.Password;
            this.Email = user.Email;
            this.Id = user.Id;
            this.Price = user.Price;
            this.Projects = new List<string>();

            if (user.Projects != null)
                foreach (Project project in user.Projects)
                {
                    this.Projects.Add(project.Name);
                }
        }

        public static List<UserOutModel> ListUser(List<User> users)
        {
            List<UserOutModel> outModel = new List<UserOutModel>();

            foreach (User user in users)
            {
                outModel.Add(new UserOutModel(user));
            }

            return outModel;
        }
        
    }
}
