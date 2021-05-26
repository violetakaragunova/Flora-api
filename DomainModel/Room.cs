using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    class Room
    {
        public int Id { get; set; }
        public int RoomNumber { get; set; }
        public ICollection<Plant> Plants { get; set; }
    }
}
