using Domain;
using System;

namespace OBLDA2.Models
{
    public class BugUpdateStateModel
    {
        public string State { get; set; }
        public int BugId { get; set; }

        public BugUpdateStateModel() { }

        public BugUpdateStateModel(string bugState, int BugId) {
            this.State = bugState;
            this.BugId = BugId;
        }

    }
}
