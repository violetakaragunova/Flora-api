using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class PlantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PlantImageDTO> Photos { get; set; }
        public ICollection<PlantNeedDTO> PlantNeeds { get; set; }
    }
}
