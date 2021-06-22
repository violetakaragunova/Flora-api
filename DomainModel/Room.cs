using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
    }
}
