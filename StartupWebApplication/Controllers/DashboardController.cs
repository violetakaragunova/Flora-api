using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlantTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashboardService _dashboardService;

        public DashboardController(IMapper mapper, IDashboardService dashboardService)
        {
            _mapper = mapper;
            _dashboardService = dashboardService;
        }

        [HttpGet("types")]
        public List<FrequencyTypeModel> GetTypes()
        {
            var types = _dashboardService.GetTypes();

            return _mapper.Map<List<FrequencyTypeModel>>(types);
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddAction([FromBody] ActionModel actionModel)
        {
            var action = _mapper.Map<ActionModel>(await _dashboardService.AddAction(_mapper.Map<ActionDTO>(actionModel)));

            return Ok(action);
        }

        [HttpGet("plants/{typeId}")]
        public List<DashboardPlantModel> GetPlants(int typeId)
         {
            var plants = _mapper.Map<List<DashboardPlantModel>>(_dashboardService.GetPlants(typeId));

            return plants;
        }
    }
}
