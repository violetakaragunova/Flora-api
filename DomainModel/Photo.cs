using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public Plant Plant { get; set; }
    }
}
