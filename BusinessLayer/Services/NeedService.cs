using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.DomainModel;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PlantTrackerAPI.BusinessLayer.Services
{
    public class NeedService : INeedService
    {
        private readonly ApplicationContext dbContext;
        private readonly IMapper _mapper;

        public NeedService(ApplicationContext _dbContext, IMapper mapper)
        {
            dbContext = _dbContext;
            _mapper = mapper;
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
        }

        public async Task<PlantNeedDTO> UpdatePlantNeed(PlantNeedDTO plantNeedDTO)
        {
            var id = plantNeedDTO.Id;
            var need = await dbContext.PlantNeeds.FirstOrDefaultAsync(r => r.Id == id);
            if (need == null)
                throw new HttpListenerException(404, "Plant need with id " + id + " does not exist");

            var plantNeed = _mapper.Map<PlantNeed>(plantNeedDTO);
            dbContext.Entry(need).CurrentValues.SetValues(plantNeed);
            await dbContext.SaveChangesAsync();

            return _mapper.Map<PlantNeedDTO>(plantNeed);
        }

        public IQueryable<NeedDTO> GetNeeds()
        {
            var query = dbContext.Needs.AsQueryable();

            return query.ProjectTo<NeedDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }
        public string GetNeedNameById(int id)
        {
            var need = dbContext.Needs.Find(id);
            return need.Name;
        }
    }
}
