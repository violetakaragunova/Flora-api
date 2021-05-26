using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    class Action
    {
        public int Id { get; set; }
        public AppUser User { get; set; }
        public Plant Plant { get; set; }
        public Need Need { get; set; }
        public DateTime DateActionDone { get; set; }
    }
}
