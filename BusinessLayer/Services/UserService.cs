using AutoMapper;
using DataAccessLayer;
using DataTransferLayer.DTO;
using DataTransferLayer.Interfaces;
using DomainModel.Identity;
using Microsoft.AspNetCore.Identity;
using PlantTrackerAPI.DomainModel;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext dbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(ApplicationContext _dbContext, IMapper mapper, UserManager<AppUser> userManager)
        {
            dbContext = _dbContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDTO> Get(int Id)
        {
            AppUser user = await _userManager.FindByIdAsync(Id.ToString()).ConfigureAwait(false);
            return _mapper.Map<UserDTO>(user);
        }
    }
}
