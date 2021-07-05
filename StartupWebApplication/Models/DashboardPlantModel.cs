using System.Collections.Generic;

namespace PlantTrackerAPI.Models
{
    public class DashboardPlantModel
    {
        public int PlantId { get; set; }
        public string RoomName { get; set; }
        public string PhotoUrl { get; set; }
        public IEnumerable<DashboardPlantNeedModel> PlantNeeds { get; set; }
    }
}
