using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
    }
}
