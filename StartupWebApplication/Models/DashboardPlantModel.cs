using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantTrackerAPI.Models
{
    public class DashboardPlantModel
    {
        public int PlantId { get; set; }
        public int NeedId { get; set; }
        public int RoomId { get; set; }
        public IQueryable<DashboardPlantModel> PlantNeeds { get; set; }
    }
}
