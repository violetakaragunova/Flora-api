using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class PlantNeedDTO
    {
        public int Id { get; set; }
        public string MonthFromName { get; set; }
        public int MonthFromId { get; set; }
        public string MonthToName { get; set; }
        public int MonthToId { get; set; }
        public int Quantity { get; set; }
        public int Frequency { get; set; }
        public int FrequencyTypeId { get; set; }
        public string FrequencyType { get; set; }
        public int NeedId { get; set; }
        public int PlantId { get; set; }
        public string NeedName { get; set; }
    }
}
