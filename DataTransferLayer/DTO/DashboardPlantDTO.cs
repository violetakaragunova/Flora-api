using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class DashboardPlantDTO
    {
        public int PlantId { get; set; }
        public string RoomName { get; set; }
        public string PhotoUrl { get; set; }
        public IEnumerable<DashboardPlantNeedDTO> PlantNeeds { get; set; }
    }
}
