using System;

namespace PlantTrackerAPI.Models
{
    public class DashboardPlantNeedModel
    {
        public string NeedName { get; set; }
        public int NeedId { get; set; }
        public int Quantity { get; set; }
        public DateTime NextActionDone { get; set; }
    }
}
