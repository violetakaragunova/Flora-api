using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class PlantImage
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public virtual Plant Plant { get; set; }
        public int PlantId { get; set; }
    }
}
