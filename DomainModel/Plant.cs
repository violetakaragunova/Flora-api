using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    class Plant
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public ICollection<Action> Actions { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<PlantNeed> PlantNeeds { get; set; }
    }
}
