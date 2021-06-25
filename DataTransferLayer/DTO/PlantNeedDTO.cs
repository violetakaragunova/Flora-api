﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class PlantNeedDTO
    {
        public int Id { get; set; }
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int Quantity { get; set; }
        public int Frequency { get; set; }
        public string FrequencyTypeId { get; set; }
        public int NeedId { get; set; }
        public int PlantId { get; set; }
        public string NeedName { get; set; }
    }
}
