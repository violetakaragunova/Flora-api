using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantTrackerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IPlantService _plantService;

        public PlantController(IMapper mapper, IPlantService plantService)
        {
            _mapper = mapper;
            _plantService = plantService;
        }

        [HttpGet]
        public List<PlantModel> GetPlants()
        {
            var plants = _plantService.GetPlants();

            return _mapper.Map<List<PlantModel>>(plants);
        }

        [HttpGet("{id}")]
        public async Task<PlantModel> GetPlantById(int id)
        {
            PlantModel plant = _mapper.Map<PlantModel>(await _plantService.GetPlantById(id).ConfigureAwait(false));
            return plant;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlantById(int id)
        {
            var result = await _plantService.DeletePlant(id);
            if (result)
                return Ok();
            else
                return BadRequest();
        }


        [HttpPost("add")]
        public async Task<ActionResult> AddPlant([FromBody] PlantAddModel plantAddModel)
        {
            var plant =_mapper.Map<PlantModel>(await _plantService.AddPlant(_mapper.Map<PlantAddDTO>(plantAddModel)));

            return Ok(plant);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdatePlant([FromBody] PlantAddModel plantAddModel)
        {
            var plant = _mapper.Map<PlantModel>(await _plantService.UpdatePlant(_mapper.Map<PlantDTO>(plantAddModel)));

            return Ok(plant);
        }
    }

}
