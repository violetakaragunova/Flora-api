using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using StartupWebApplication.Models;
using PlantTrackerAPI.Models;

namespace PlantTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public AccountController(IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _accountService = accountService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            var user = _mapper.Map<ResponseLoginModel>(await _accountService.RegisterUser(_mapper.Map<RegisterDTO>(registerModel)));

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> Login(RequestLoginModel loginModel)
        {
            var user = _mapper.Map<ResponseLoginModel>(await _accountService.LogInUser(_mapper.Map<LoginDTO>(loginModel)));

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("forgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordModel forgotPasswordModel)
        {
            var result = await _accountService.ForgetPassword(_mapper.Map<ForgotPasswordRequestDTO>(forgotPasswordModel));

            return Ok(result);
        }

        [AllowAnonymous]
        [HttpPost("resetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordModel)
        {
            var result = await _accountService.ResetPassword(_mapper.Map<ResetPasswordDto>(resetPasswordModel));

            return Ok(result);
        }
    }

}
