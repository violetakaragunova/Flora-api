﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class DashboardPlant
    {
        public int PlantId { get; set; }
        public int RoomId { get; set; }
        public IEnumerable<DashboardPlantNeed> PlantNeeds { get; set; }
    }
}
