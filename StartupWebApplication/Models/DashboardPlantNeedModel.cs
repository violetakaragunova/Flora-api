using System;

namespace PlantTrackerAPI.Models
{
    public class DashboardPlantNeedModel
    {
        public string NeedName { get; set; }
        public int NeedId { get; set; }
        public int PlantId { get; set; }
        public int Quantity { get; set; }
        public DateTime NextAction { get; set; }
        public DateTime LastActionDone { get; set; }
        public string LastActionDoneBy { get; set; }
        public bool shouldDisplay { get; set; }
    }
}
