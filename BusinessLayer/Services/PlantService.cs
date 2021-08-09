﻿using AutoMapper;
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
            var plants = dbContext.Plants.Include(p => p.Photos).Include(n => n.PlantNeeds).AsQueryable();

            return plants.ProjectTo<PlantDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }

        public async Task<PlantDTO> GetPlantById(int id)
        {
            IQueryable<Month> months = dbContext.Months.AsQueryable();
            IQueryable<Need> needs = dbContext.Needs.AsQueryable();
            IQueryable<FrequencyType> frequencyTypes = dbContext.FrequencyTypes.AsQueryable();

            Plant plant = await dbContext.Plants.Include(p => p.Photos).Include(n => n.PlantNeeds).SingleOrDefaultAsync(x => x.Id == id);

            if(plant == null)
                throw new HttpListenerException(404,"Plant with id "+id+" does not exist");

            var mappedPlant = _mapper.Map<PlantDTO>(plant);

            foreach (var need in mappedPlant.PlantNeeds)
            {
                need.MonthFromName = months.FirstOrDefault(x => x.Id == need.MonthFromId).Name;
                need.MonthToName = months.FirstOrDefault(x => x.Id == need.MonthToId).Name;
                need.NeedName = needs.FirstOrDefault(x => x.Id == need.NeedId).Name;
                need.FrequencyType = frequencyTypes.FirstOrDefault(x => x.Id == need.FrequencyTypeId).Type;
            }

            return mappedPlant;
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

        public async Task<PlantDTO> AddPlant(PlantAddDTO plantDTO)
        {
            var roomId = plantDTO.RoomId;
            var room =await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                throw new HttpListenerException(404, "Room with id " + roomId + " does not exist");
            var plant = _mapper.Map<Plant>(plantDTO);
            dbContext.Plants.Add(plant);
            await dbContext.SaveChangesAsync();
            var image = new PlantImage
            {
                Url = plantDTO.Url,
                IsMain = true,
                PlantId = plant.Id
            };

            dbContext.PlantImages.Add(image);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<PlantDTO>(plant);
        }

        public async Task<PlantDTO> UpdatePlant(PlantAddDTO plantDTO)
        {
            var plantFromData = dbContext.Plants.Find(plantDTO.Id);
            if(plantFromData == null)
                throw new HttpListenerException(404, "Plant with id " + plantDTO.Id + " does not exist");

            var roomId = plantDTO.RoomId;
            var room = await dbContext.Rooms.FirstOrDefaultAsync(r => r.Id == roomId);
            if (room == null)
                throw new HttpListenerException(404, "Room with id " + roomId + " does not exist");

            plantFromData.Name = plantDTO.Name;
            plantFromData.Description = plantDTO.Description;
            plantFromData.RoomId = roomId;

            dbContext.Plants.Update(plantFromData);

            await dbContext.SaveChangesAsync();

            return await GetPlantById(plantFromData.Id);
        }
    }
}
