using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NeedController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INeedService _needService;

        public NeedController(IMapper mapper, INeedService needService)
        {
            _mapper = mapper;
            _needService = needService;
        }

        [HttpGet]
        public List<NeedModel> GetNeeds()
        {
            var needs = _needService.GetNeeds();

            return _mapper.Map<List<NeedModel>>(needs);
        }

        [HttpPost("update")]
        public async Task<ActionResult> UpdatePlantNeed([FromBody] PlantNeedModel plantNeedModel)
        {
            var plantNeed = _mapper.Map<PlantNeedModel>(await _needService.UpdatePlantNeed(_mapper.Map<PlantNeedDTO>(plantNeedModel)));

            return Ok(plantNeed);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNeedById(int id)
        {
            var result = await _needService.DeleteNeed(id);
            if (result)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost("add")]
        public async Task<ActionResult> AddPlantNeed([FromBody] PlantNeedModel plantNeedModel)
        {
            var plantNeed = _mapper.Map<PlantNeedModel>(await _needService.AddPlantNeed(_mapper.Map<PlantNeedDTO>(plantNeedModel)));

            return Ok(plantNeed);
        }

        [HttpGet("{id}")]
        public string GetNeedById(int id)
        {
            return _needService.GetNeedNameById(id);
        }
    }
}
