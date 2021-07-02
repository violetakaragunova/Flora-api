using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class DashboardPlantNeedDTO
    {
        public int Quantity { get; set; }
        public DateTime NextActionDone { get; set; }
    }
}
