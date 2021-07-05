using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class DashboardPlantNeedDTO
    {
        public string NeedName { get; set; }
        public int NeedId { get; set; }
        public int Quantity { get; set; }
        public DateTime NextActionDone { get; set; }
    }
}
