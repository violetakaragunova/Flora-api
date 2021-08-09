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

            actionDTO.DateActionDone = DateTime.Today;

            var action = _mapper.Map<DomainModel.Action>(actionDTO);
            dbContext.Actions.Add(action);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<ActionDTO>(action);
        }

        public List<DashboardPlantDTO> GetPlants(int typeId)
        {
            int curMonth = DateTime.Now.Month;
            DateTime startDate;
            DateTime endDate;
            if(typeId == 1)
            {
                endDate = DateTime.Today;
            }
            else if(typeId == 2)
            {
                DateTime ClockInfoFromSystem = DateTime.Today;
                int dayToday = (int)ClockInfoFromSystem.DayOfWeek;
                startDate = DateTime.Today.AddDays(-dayToday + 1);
                endDate = startDate.AddDays(6);
            } else
            {
                DateTime ClockInfoFromSystem = DateTime.Today;
                startDate = new DateTime(ClockInfoFromSystem.Year, ClockInfoFromSystem.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            

            DateTime today = DateTime.Today;

            var LastActions = (from action in dbContext.Actions
                               join users in dbContext.Users
                               on action.UserId equals users.Id
                               group action by new
                               {
                                   plantId = action.PlantId,
                                   needId = action.NeedId,
                                   useerId= users.Id,
                                   userName = users.UserName
                               } into g
                               select new ActionDTO
                               {
                                   PlantId = g.Key.plantId,
                                   NeedId = g.Key.needId,
                                   UserId = g.Key.useerId,
                                   UserName = g.Key.userName,
                                   DateActionDone = g.Max(a => a.DateActionDone)
                               }).Distinct().AsQueryable();

            var result = (from plants in dbContext.Plants
                          join plantNeeds in dbContext.PlantNeeds
                          on plants.Id equals plantNeeds.PlantId
                          where curMonth >= plantNeeds.MonthFromId && curMonth<=plantNeeds.MonthToId
                          join needs in dbContext.Needs
                          on plantNeeds.NeedId equals needs.Id
                          join frequencyType in dbContext.FrequencyTypes
                          on plantNeeds.FrequencyTypeId equals frequencyType.Id
                          join rooms in dbContext.Rooms
                          on plants.RoomId equals rooms.Id
                          join action in LastActions
                          on new { plantId = plants.Id, needId = plantNeeds.NeedId } equals new { plantId = action.PlantId, needId = action.NeedId } into PlantsWithLastNeeds
                          from plantsWithLastNeeds in PlantsWithLastNeeds.DefaultIfEmpty()
                          where plantsWithLastNeeds.DateActionDone!=today
                          select new
                          {
                              PlantId = plantNeeds.PlantId,
                              RoomName = rooms.RoomName,
                              NeedId = plantNeeds.NeedId,
                              NeedName = needs.Name,
                              NextAction = plantsWithLastNeeds.DateActionDone.AddDays(plantNeeds.Frequency * frequencyType.Days),
                              Quantity = plantNeeds.Quantity,
                              PhotoUrl = plants.Photos.FirstOrDefault(x => x.IsMain == true).Url,
                              Name = plants.Name,
                              Description = plants.Description,
                              LastActionDone = plantsWithLastNeeds.DateActionDone,
                              LastActionDoneBy = plantsWithLastNeeds.UserName
                          } into plantNeedsWithAction
                          select plantNeedsWithAction).ToList();


            var resultList = result
                .GroupBy(d => new
                {
                    PlantId = d.PlantId,
                    RoomName = d.RoomName,
                    PhotoUrl = d.PhotoUrl,
                    Name= d.Name,
                    Description = d.Description
                })
                .Select(d => new DashboardPlant
                {
                    PlantId = d.Key.PlantId,
                    RoomName = d.Key.RoomName,
                    PhotoUrl = d.Key.PhotoUrl,
                    Name = d.Key.Name,
                    Description=d.Key.Description,
                    PlantNeeds = result.Where(x => x.PlantId == d.Key.PlantId && x.NextAction <= endDate)
                    .Select(x => new DashboardPlantNeed
                    {
                        NeedName=x.NeedName,
                        NeedId = x.NeedId,
                        PlantId = x.PlantId,
                        Quantity = x.Quantity,
                        NextAction = x.NextAction,
                        LastActionDone = x.LastActionDone,
                        LastActionDoneBy  = x.LastActionDoneBy
                    }).ToList()
                }).ToList();

            return _mapper.Map<List<DashboardPlantDTO>>(resultList);
        }

        public List<DashboardPlantDTO> GetPlantsX(int typeId)
        {
            int curMonth = DateTime.Now.Month;
            DateTime startDate;
            DateTime endDate;
            if (typeId == 1)
            {
                endDate = DateTime.Today;
            }
            else if (typeId == 2)
            {
                DateTime ClockInfoFromSystem = DateTime.Today;
                int dayToday = (int)ClockInfoFromSystem.DayOfWeek;
                startDate = DateTime.Today.AddDays(-dayToday + 1);
                endDate = startDate.AddDays(6);
            }
            else
            {
                DateTime ClockInfoFromSystem = DateTime.Today;
                startDate = new DateTime(ClockInfoFromSystem.Year, ClockInfoFromSystem.Month, 1);
                endDate = startDate.AddMonths(1).AddDays(-1);
            }
            DateTime today = DateTime.Today;

            var rows = dbContext.Actions
                       .FromSqlRaw(
                                   @"select lastAction.PlantId,lastAction.NeedId,lastAction.userId,lastAction.DateActionDone 
                                   from(select la.PlantId,la.NeedId,a.UserId,a.DateActionDone 
                                   from [dbo].[Actions] as a
                                   right join (select distinct pn.PlantId,pn.NeedId,MAX(a.DateActionDone) as DateActionDone
                                   from [dbo].[PlantNeeds] as pn
                                   left join (select *
		                           from [dbo].[Actions])as a
                                   on a.PlantId=pn.PlantId and a.NeedId=pn.NeedId
                                   where (SELECT MONTH(GETDATE())) between MonthFromId and MonthToId
                                   group by pn.PlantId,pn.NeedId) as la
                                   on a.PlantId=la.PlantId and a.NeedId=la.NeedId and a.DateActionDone=la.DateActionDone) as lastAction");


            var result = (from plants in dbContext.Plants
                          join plantNeeds in dbContext.PlantNeeds
                          on plants.Id equals plantNeeds.PlantId
                          where curMonth >= plantNeeds.MonthFromId && curMonth <= plantNeeds.MonthToId
                          join needs in dbContext.Needs
                          on plantNeeds.NeedId equals needs.Id
                          join frequencyType in dbContext.FrequencyTypes
                          on plantNeeds.FrequencyTypeId equals frequencyType.Id
                          join rooms in dbContext.Rooms
                          on plants.RoomId equals rooms.Id
                          join action in rows
                          on new { plantId = plants.Id, needId = plantNeeds.NeedId } equals new { plantId = action.PlantId, needId = action.NeedId } into PlantsWithLastNeeds
                          from plantsWithLastNeeds in PlantsWithLastNeeds.DefaultIfEmpty()
                          select new
                          {
                              PlantId = plantNeeds.PlantId,
                              RoomName = rooms.RoomName,
                              NeedId = plantNeeds.NeedId,
                              NeedName = needs.Name,
                              NextAction = plantsWithLastNeeds.DateActionDone.AddDays(plantNeeds.Frequency * frequencyType.Days),
                              Quantity = plantNeeds.Quantity,
                              PhotoUrl = plants.Photos.FirstOrDefault(x => x.IsMain == true).Url,
                              Name = plants.Name,
                              Description = plants.Description,
                              LastActionDone = plantsWithLastNeeds.DateActionDone,
                              LastActionDoneById = plantsWithLastNeeds.UserId
                          } into plantNeedsWithAction
                          select plantNeedsWithAction).ToList();


            var resultList = result
                .GroupBy(d => new
                {
                    PlantId = d.PlantId,
                    RoomName = d.RoomName,
                    PhotoUrl = d.PhotoUrl,
                    Name = d.Name,
                    Description = d.Description
                })
                .Select(d => new DashboardPlant
                {
                    PlantId = d.Key.PlantId,
                    RoomName = d.Key.RoomName,
                    PhotoUrl = d.Key.PhotoUrl,
                    Name = d.Key.Name,
                    Description = d.Key.Description,
                    PlantNeeds = result.Where(x => x.PlantId == d.Key.PlantId)
                    .Select(x => new DashboardPlantNeed
                    {
                        NeedName = x.NeedName,
                        NeedId = x.NeedId,
                        PlantId = x.PlantId,
                        Quantity = x.Quantity,
                        NextAction = x.NextAction,
                        LastActionDone = x.LastActionDone,
                        LastActionDoneBy = x.LastActionDoneById != 0 ? dbContext.Users.FirstOrDefault( a => a.Id == x.LastActionDoneById).UserName : null,
                        shouldDisplay = (x.NextAction < endDate && x.LastActionDone != today) ? true : false
                    }).ToList()
                }).ToList();

            return _mapper.Map<List<DashboardPlantDTO>>(resultList);
        }
    }
}
