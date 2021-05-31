using AutoMapper;
using DataTransferLayer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.DomainModel;
using System.Threading.Tasks;

namespace PlantTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper) { 
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var userExists = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == registerDTO.Username.ToLower());

            if (userExists != null)
            {
                return BadRequest("Username is taken.");
            }

            var user = _mapper.Map<User>(registerDTO);

            user.UserName = registerDTO.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            var userDto =  new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };

            return Ok(userDto);
        }
           
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());

            if( user == null)
            {
                return Unauthorized("Invalid username");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if(!result.Succeeded)
            {
                return Unauthorized("Invalid password");
            }

            var userDTO = new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };

            return Ok(userDTO);
        }
    }
}
