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

        [HttpGet]
        public List<FrequencyTypeModel> GetTypes()
        {
            var types = _dashboardService.GetTypes();

            return _mapper.Map<List<FrequencyTypeModel>>(types);
        }

        [HttpGet("action/{needId}/{plantId}/{type}")]
        public bool NeedAction(int needId, int plantId, int type)
        {
            DateTime curDate = DateTime.Now;

            var lastAction = _dashboardService.CheckLastAction(needId , plantId , type);

            /*DateTime nextAction = lastAction.AddDays(1);

            if (lastAction == null)
                return true;

            if (nextAction <= curDate)
                return true;
            else
                return false;*/

            return true;
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddAction([FromBody] ActionModel actionModel)
        {
            var action = _mapper.Map<ActionModel>(await _dashboardService.AddAction(_mapper.Map<ActionDTO>(actionModel)));

            return Ok(action);
        }

        [HttpGet("plants")]
        public List<DashboardPlantModel> GetPlants()
        {
            var plants = _mapper.Map<List<DashboardPlantModel>>(_dashboardService.GetPlants());

            return plants;
        }
    }
}
