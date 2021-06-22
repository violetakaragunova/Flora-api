using PlantTrackerAPI.DataTransferLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantTrackerAPI.DataTransferLayer.Interfaces
{
    public interface INeedService
    {
        public Task<PlantNeedDTO> UpdatePlantNeed(PlantNeedDTO plantNeedDTO);
        public IQueryable<NeedDTO> GetNeeds();
        public string GetNeedNameById(int id);
        public Task<PlantNeedDTO> AddPlantNeed(PlantNeedDTO plantNeedDTO);
        public Task<bool> DeleteNeed(int id);
    }
}
