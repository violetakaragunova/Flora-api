using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class MonthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMonthService _monthService;

        public MonthController(IMapper mapper, IMonthService montService)
        {
            _mapper = mapper;
            _monthService = montService;
        }

        [HttpGet]
        public List<MonthModel> GetMonths()
        {
            var months = _monthService.GetMonths();

            return _mapper.Map<List<MonthModel>>(months);
        }
    }
}
