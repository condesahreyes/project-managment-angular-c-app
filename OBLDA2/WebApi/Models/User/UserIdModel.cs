using System;
using System.Collections.Generic;
using Domain;

namespace OBLDA2.Models
{
    public class UserIdModel
    {
        public Guid UserId { get; set; }

        public UserIdModel() { }

        public UserIdModel(Guid userId)
        {
            this.UserId = userId;
        }

    }
}
