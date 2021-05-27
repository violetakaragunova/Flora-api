using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class PlantNeed
    {
        public int Id { get; set; }
        public int MonthFrom { get; set; }
        public int MonthTo { get; set; }
        public int Quantity { get; set; }
        public int Frequency { get; set; }
        public string FrequencyType { get; set; }
        public virtual Need Need { get; set; }
        public int NeedId { get; set; }
        public virtual Plant Plant { get; set; }
        public int PlantId { get; set; }
    }
}
