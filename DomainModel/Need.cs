using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class Need
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Action> Actions { get; set; }
        public ICollection<PlantNeed> PlantNeeds { get; set; }
    }
}
