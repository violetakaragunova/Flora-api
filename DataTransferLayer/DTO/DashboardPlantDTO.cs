using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class DashboardPlantDTO
    {
        public int PlantId { get; set; }
        public int NeedId { get; set; }
        public int RoomId { get; set; }
        public IQueryable<DashboardPlantNeedDTO> PlantNeeds { get; set; }
    }
}
