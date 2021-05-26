using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    class PlantNeed
    {
        public int Id { get; set; }
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int Quantity { get; set; }
        public int Frequency { get; set; }
        public string FrequencyType { get; set; }
        public Need Need { get; set; }
        public Plant Plant { get; set; }
    }
}
