using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.DTO
{
    public class ActionDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int PlantId { get; set; }
        public int NeedId { get; set; }
        public DateTime DateActionDone { get; set; }
    }
}
