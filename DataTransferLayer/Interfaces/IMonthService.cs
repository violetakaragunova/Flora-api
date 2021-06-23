using PlantTrackerAPI.DataTransferLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlantTrackerAPI.DataTransferLayer.Interfaces
{
    public interface IMonthService
    {
        public IQueryable<MonthDTO> GetMonths();
    }
}
