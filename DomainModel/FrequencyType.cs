using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class FrequencyType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<PlantNeed> PlantNeeds { get; set; }
    }
}
