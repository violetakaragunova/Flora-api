using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using System;
using System.Linq;

namespace PlantTrackerAPI.BusinessLayer.Services
{
    public class RoomService : IRoomService
    {
        private readonly ApplicationContext dbContext;
        private readonly IMapper _mapper;

        public RoomService(ApplicationContext _dbContext, IMapper mapper)
        {
            dbContext = _dbContext;
            _mapper = mapper;
        }

        public IQueryable<RoomDTO> GetRooms()
        {
            var rooms = dbContext.Rooms.AsQueryable();

            return rooms.ProjectTo<RoomDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }
    }
}
