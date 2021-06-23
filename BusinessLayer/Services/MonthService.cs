using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using System.Linq;

namespace PlantTrackerAPI.BusinessLayer.Services
{
    public class MonthService : IMonthService
    {
        private readonly ApplicationContext dbContext;
        private readonly IMapper _mapper;

        public MonthService(ApplicationContext _dbContext, IMapper mapper)
        {
            dbContext = _dbContext;
            _mapper = mapper;
        }

        public IQueryable<MonthDTO> GetMonths()
        {
            var query1 = dbContext.Months.AsQueryable();

            return query1.ProjectTo<MonthDTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }
    }
}
