using PlantTrackerAPI.DataTransferLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantTrackerAPI.DataTransferLayer.Interfaces
{
    public interface IDashboardService
    {
        public IQueryable<FrequencyTypeDTO> GetTypes();
        public DateTime CheckLastAction(int needId , int plantId , int type);
        public Task<ActionDTO> AddAction(ActionDTO actionDTO);
        public List<DashboardPlantDTO> GetPlants();

    }
}
