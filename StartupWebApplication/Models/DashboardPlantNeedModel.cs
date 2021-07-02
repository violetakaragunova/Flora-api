using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantTrackerAPI.Models
{
    public class DashboardPlantNeedModel
    {
        public int Quantity { get; set; }
        public DateTime NextActionDone { get; set; }
    }
}
