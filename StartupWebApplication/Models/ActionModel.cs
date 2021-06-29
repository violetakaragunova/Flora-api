using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantTrackerAPI.Models
{
    public class ActionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PlantId { get; set; }
        public int NeedId { get; set; }
        public DateTime DateActionDone { get; set; }
    }
}
