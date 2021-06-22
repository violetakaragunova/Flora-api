using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PlantTrackerAPI.DataTransferLayer.DTO;
using System.Linq;
using System.Threading.Tasks;

namespace PlantTrackerAPI.DataTransferLayer.Interfaces
{
    public interface IPlantService
    {
        public IQueryable<PlantDTO> GetPlants();
        public Task<PlantDTO> GetPlantById(int id);

        public Task<bool> DeletePlant(int id);
        public Task<PlantDTO> AddPlant(PlantDTO plantDTO);
        public Task<PlantDTO> UpdatePlant(PlantDTO plantDTO);

    }
}
