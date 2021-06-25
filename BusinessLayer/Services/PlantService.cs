using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PlantTrackerAPI.BusinessLayer.Services
{
    public class PlantService : IPlantService
    {
        private readonly ApplicationContext dbContext;
        private readonly IMapper _mapper;

        public PlantService(ApplicationContext _dbContext, IMapper mapper)
        {
            dbContext = _dbContext;
            _mapper = mapper;
        }
        public IQueryable<PlantDTO> GetPlants()
        {
            var query1 = dbContext.Plants.Include(p => p.Photos).Include(n => n.PlantNeeds).AsQueryable();

            return query1.ProjectTo<PlantDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }

        public async Task<PlantDTO> GetPlantById(int id)
        {
            Plant plant = await dbContext.Plants.Include(p => p.Photos).Include(n => n.PlantNeeds).SingleOrDefaultAsync(x => x.Id == id);
            if(plant == null)
                throw new HttpListenerException(404,"Plant with id "+id+" does not exist");

            return _mapper.Map<PlantDTO>(plant);
        }

        public async Task<bool> DeletePlant(int id)
        {
            Plant plant = await dbContext.Plants.SingleOrDefaultAsync(p => p.Id == id);
            if (plant == null)
                throw new HttpListenerException(404, "Plant with id " + id + " does not exist");

            dbContext.Plants.Remove(plant);

            await dbContext.SaveChangesAsync();

            return true;

        }

        public async Task<PlantDTO> AddPlant(PlantDTO plantDTO)
        {
            var roomId = plantDTO.RoomId;
            var room =await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                throw new HttpListenerException(404, "Room with id " + roomId + " does not exist");
            var plant = _mapper.Map<Plant>(plantDTO);
            dbContext.Plants.Add(plant);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<PlantDTO>(plant);
        }

        public async Task<PlantDTO> UpdatePlant(PlantDTO plantDTO)
        {
            var plantFromData = dbContext.Plants.Find(plantDTO.Id);
            if(plantFromData == null)
                throw new HttpListenerException(404, "Plant with id " + plantDTO.Id + " does not exist");

            plantFromData.Name = plantDTO.Name;
            plantFromData.Description = plantDTO.Description;
            plantFromData.RoomId = plantDTO.RoomId;

            dbContext.Plants.Update(plantFromData);

            await dbContext.SaveChangesAsync();

            return await GetPlantById(plantFromData.Id);
        }
    }
}
