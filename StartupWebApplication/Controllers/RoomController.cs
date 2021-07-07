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
    public class RoomController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoomService _roomService;

        public RoomController(IMapper mapper, IRoomService roomService)
        {
            _mapper = mapper;
            _roomService = roomService;
        }

        [HttpGet]
        public List<RoomModel> GetMonths()
        {
            var rooms = _roomService.GetRooms();

            return _mapper.Map<List<RoomModel>>(rooms);
        }
    }
}
