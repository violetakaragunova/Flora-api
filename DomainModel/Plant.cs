using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class Plant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PlantFamilyName { get; set; }
        public string Description { get; set; }
        public virtual Room Room { get; set; }
        public int RoomId { get; set; }
        public ICollection<Action> Actions { get; set; }
        public ICollection<PlantImage> Photos { get; set; }
        public ICollection<PlantNeed> PlantNeeds { get; set; }
    }
}
