using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Domain;

namespace OBLDA2.Models
{
    [ExcludeFromCodeCoverage]
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
