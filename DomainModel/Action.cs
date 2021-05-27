using System;
using System.Collections.Generic;
using System.Text;

namespace PlantTrackerAPI.DomainModel
{
    public class Action
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Plant Plant { get; set; }
        public int PlantId { get; set; }
        public virtual Need Need { get; set; }
        public int NeedId { get; set; }
        public DateTime DateActionDone { get; set; }
    }
}
