using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class Month
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlantNeed> PlantNeedsFrom { get; set; }
        public ICollection<PlantNeed> PlantNeedsTo { get; set; }
    }
}
