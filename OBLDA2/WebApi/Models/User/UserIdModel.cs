using System;
using System.Collections.Generic;
using Domain;

namespace OBLDA2.Models
{
    public class UserIdModel
    {
        public Guid Id { get; set; }

        public UserIdModel() { }

        public UserIdModel(Guid userId)
        {
            this.Id = userId;
        }

    }
}
