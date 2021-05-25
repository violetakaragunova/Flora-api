using AutoMapper;
using DataTransferLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StartupWebApplication.Models;
using System.Threading.Tasks;

namespace StartupWebApplication.Controllers
{
    [ApiVersion("1.0")]    
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/v{v:apiVersion}/User/GetUser/{id}")]
        public async Task<IActionResult> GetUser(int Id)
        {
            if(string.IsNullOrWhiteSpace(Id.ToString()))
            {
                return BadRequest();
            }

            UserModel user = _mapper.Map<UserModel>(await _userService.Get(Id).ConfigureAwait(false));

            return CreatedAtAction("GetById", new { id = user.Id }, user);
        }
    }
}
