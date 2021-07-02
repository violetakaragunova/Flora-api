using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class DashboardPlantNeed
    {
        public int NeedId { get; set; }
        public int Quantity { get; set; }
        public DateTime NextActionDone { get; set; }
    }
}
