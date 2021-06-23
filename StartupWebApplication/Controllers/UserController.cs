using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataTransferLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StartupWebApplication.Models;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StartupWebApplication.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]    
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<UserModel> GetUser(int Id)
        {
            UserModel user = _mapper.Map<UserModel>(await _userService.GetUserById(Id).ConfigureAwait(false));

            return user;
        }

        [HttpGet]
        public IQueryable<UserModel> GetAllUsers()
        {
            var users = _userService.GetUsers();
            return users.ProjectTo<UserModel>(_mapper.ConfigurationProvider).AsNoTracking();
        }
    }
}
