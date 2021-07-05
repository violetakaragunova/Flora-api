using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantTrackerAPI.Models
{
    public class PlantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RoomName { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PlantImageModel> Photos { get; set; }
        public ICollection<PlantNeedModel> PlantNeeds { get; set; }
    }
}
