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

        /*public string GetNeedNameById(int id)
        {
            var need = dbContext.Needs.Find(id);
            return need.Name;
        }

        public async Task<PlantNeedDTO> AddPlantNeed(PlantNeedDTO plantNeedDTO)
        {
            var needId = plantNeedDTO.NeedId;
            var need = await dbContext.Needs.FirstOrDefaultAsync(r => r.Id == needId);
            if (need == null)
                throw new HttpListenerException(404, "Need with id " + needId + " does not exist");

            var plantNeed = _mapper.Map<PlantNeed>(plantNeedDTO);
            dbContext.PlantNeeds.Add(plantNeed);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<PlantNeedDTO>(plantNeed);
        }
        
        public async Task<bool> DeleteNeed(int id)
        {
            PlantNeed plantNeed = await dbContext.PlantNeeds.SingleOrDefaultAsync(p => p.Id == id);
            if (plantNeed == null)
                throw new HttpListenerException(404, "PlantNeed with id " + id + " does not exist");

            dbContext.PlantNeeds.Remove(plantNeed);

            await dbContext.SaveChangesAsync();

            return true;
        }*/

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

        /*public async Task<PlantNeedDTO> UpdatePlantNeed(PlantNeedDTO plantNeedDTO)
        {
            var id = plantNeedDTO.Id;
            var need = dbContext.PlantNeeds.FirstOrDefaultAsync(r => r.Id == id);
            if (need == null)
                throw new HttpListenerException(404, "Plant need with id " + id + " does not exist");

            var plantNeed = _mapper.Map<PlantNeed>(plantNeedDTO);
            dbContext.PlantNeeds.Update(plantNeed);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<PlantNeedDTO>(plantNeed);
        }

        public IQueryable<NeedDTO> GetNeeds()
        {
            var query1 = dbContext.Needs.AsQueryable();

            return query1.ProjectTo<NeedDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }*/
    }
}
