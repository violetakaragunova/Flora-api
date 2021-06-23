using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class PlantImageDTO
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public int PlantId { get; set; }
    }
}
