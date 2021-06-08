using AutoMapper;
using DataTransferLayer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.DomainModel;
using System.Threading.Tasks;
using PlantTrackerAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using System;
using log4net;
using System.Reflection;
using System.Net;

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
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService, IMapper mapper) { 
            _tokenService = tokenService;
            _userManager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            var userExists = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == registerDTO.Username.ToLower());

            if (userExists != null)
            {
                throw new HttpListenerException(500, "Username is taken");
            }

            var user = _mapper.Map<User>(registerDTO);

            user.UserName = registerDTO.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
                throw new HttpListenerException(500, "User is not registered");

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
                throw new HttpListenerException(500, "Errors while adding roles");

            var userDto =  new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user),
            };

            return Ok(userDto);
        }
           
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());

            if( user == null)
                throw new HttpListenerException(500, "User does not exist");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if(!result.Succeeded)
                throw new HttpListenerException(500, "Invalid passwords");

            var userDTO = new UserDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };

            return Ok(userDTO);
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO forgotPasswordRequestDTO)
        {
                var user = await _userManager.FindByEmailAsync(forgotPasswordRequestDTO.Email);

                if (user == null)
                {
                    throw new HttpListenerException(500,"User does not exist");
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var token1 = token.Replace("+","%2B");
                var link = "http://localhost:4200/#/account/resetPassword?token="+token1+"&email="+forgotPasswordRequestDTO.Email;

                EmaillHelper emailHelper = new EmaillHelper();
                bool emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

                if (emailResponse)
                    return Ok(emailResponse);
                else
                {
                    return BadRequest(emailResponse.ToString());
                }

        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDTO)
        {

            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
                throw new HttpListenerException(500, "User does not exist");

            var ressetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);

            if(!ressetPassResult.Succeeded)
            {
                throw new HttpListenerException(500, "Password was not changed");
            }

            return Ok();
        }
    }

}
