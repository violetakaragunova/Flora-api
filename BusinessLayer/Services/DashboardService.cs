using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PlantTrackerAPI.BusinessLayer.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationContext dbContext;
        private readonly IMapper _mapper;

        public DashboardService(ApplicationContext _dbContext, IMapper mapper)
        {
            dbContext = _dbContext;
            _mapper = mapper;
        }

        public IQueryable<FrequencyTypeDTO> GetTypes()
        {
            var types = dbContext.FrequencyTypes.AsQueryable();

            return types.ProjectTo<FrequencyTypeDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }

        public DateTime CheckLastAction(int needId, int plantId, int typeId)
        {
            var query1 = (from a in dbContext.Actions
                          where a.PlantId == plantId && a.NeedId == needId
                          orderby a.DateActionDone descending
                          select a.DateActionDone).Take(1).SingleOrDefault();

            return query1;
        }

        public async Task<ActionDTO> AddAction(ActionDTO actionDTO)
        {
            var plantId = actionDTO.PlantId;
            var plant = await dbContext.Plants.FirstOrDefaultAsync(r => r.Id == plantId);
            if (plant == null)
                throw new HttpListenerException(404, "Plant with id " + plantId + " does not exist");

            var needId = actionDTO.NeedId;
            var need = await dbContext.Needs.FirstOrDefaultAsync(r => r.Id == needId);
            if (need == null)
                throw new HttpListenerException(404, "Need with id " + needId + " does not exist");

            var userId = actionDTO.UserId;
            var user = await dbContext.Users.FirstOrDefaultAsync(r => r.Id == userId);
            if (user == null)
                throw new HttpListenerException(404, "User with id " + userId + " does not exist");

            var action = _mapper.Map<DomainModel.Action>(actionDTO);
            dbContext.Actions.Add(action);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<ActionDTO>(action);
        }

        public List<DashboardPlantDTO> GetPlants()
        {
            int curMonth = DateTime.Now.Month;

            DateTime today = DateTime.Now;

            var LastActions = (from action in dbContext.Actions
                               group action by new
                               {
                                   plantId = action.PlantId,
                                   needId = action.NeedId
                               } into g
                               select new ActionDTO
                               {
                                   PlantId = g.Key.plantId,
                                   NeedId = g.Key.needId,
                                   DateActionDone = g.Max(a => a.DateActionDone)
                               }).Distinct().AsQueryable();

            var result = (from plants in dbContext.Plants
                          join plantNeeds in dbContext.PlantNeeds
                          on plants.Id equals plantNeeds.PlantId
                          where curMonth >= plantNeeds.MonthFrom && curMonth<=plantNeeds.MonthTo
                          join action in LastActions
                          on new { plantId = plants.Id, needId = plantNeeds.NeedId } equals new { plantId = action.PlantId, needId = action.NeedId } into PlantsWithLastNeeds
                          from plantsWithLastNeeds in PlantsWithLastNeeds.DefaultIfEmpty()
                          select new
                          {
                              PlantId = plantNeeds.PlantId,
                              RoomId = plants.RoomId,
                              NeedId = plantNeeds.NeedId,
                              NextAction = plantsWithLastNeeds.DateActionDone.AddDays(plantNeeds.Frequency*plantNeeds.FrequencyTypeId),
                              Quantity = plantNeeds.Quantity
                          } into plantNeedsWithAction
                          select plantNeedsWithAction).ToList();


            var resultList = result
                .GroupBy(d => new 
                {
                    PlantId = d.PlantId,
                    RoomId = d.RoomId
                })
                .Select(d => new DashboardPlant
                {
                    PlantId = d.Key.PlantId,
                    RoomId = d.Key.RoomId,
                    PlantNeeds = result.Where(x => x.PlantId == d.Key.PlantId && x.NextAction <= today)
                    .Select(x => new DashboardPlantNeed
                    {
                        NeedId = x.NeedId,
                        Quantity = x.Quantity,
                        NextActionDone = x.NextAction
                    }).ToList()
                }).ToList();

            return _mapper.Map<List<DashboardPlantDTO>>(resultList);
        }
    }
}
