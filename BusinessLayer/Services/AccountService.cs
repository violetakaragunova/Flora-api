using AutoMapper;
using BusinessLayer.Helpers;
using DataTransferLayer.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.Interfaces;
using PlantTrackerAPI.DomainModel;
using System.Net;
using System.Threading.Tasks;



namespace PlantTrackerAPI.BusinessLayer.Services
{
    public class AccountService : IAccountService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<bool> ForgetPassword(ForgotPasswordRequestDTO forgotPasswordRequestDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordRequestDTO.Email);

            if (user == null)
                throw new HttpListenerException(500, "User does not exist");

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var token1 = token.Replace("+", "%2B");
            var link = "http://localhost:4200/#/account/resetPassword?token=" + token1 + "&email=" + forgotPasswordRequestDTO.Email;

            EmaillHelper emailHelper = new EmaillHelper();
            var emailResponse = emailHelper.SendEmailPasswordReset(user.Email, link);

            return emailResponse;

        }

        public async Task<UserDTO> LogInUser(LoginDTO loginDTO)
        {
            var userExists = await UserExists(loginDTO.Username);
            if (!userExists)
            {
                throw new HttpListenerException(404, "Username does not exist");
            }
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == loginDTO.Username.ToLower());
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

            if (!result.Succeeded)
                throw new HttpListenerException(500, "Invalid passwords");

            var userDTO = new UserDTO
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };

            return userDTO;
        }

        public async Task<UserDTO> RegisterUser(RegisterDTO registerDTO)
        {
            var userExists = await UserExists(registerDTO.Username);
            if (userExists)
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

            var userDto = new UserDTO
            {
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };

            return userDto;
        }

        public async Task<bool> ResetPassword(ResetPasswordDto resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user == null)
                throw new HttpListenerException(500, "User does not exist");

            var ressetPassResult = await _userManager.ResetPasswordAsync(user, resetPasswordDTO.Token, resetPasswordDTO.NewPassword);

            if (!ressetPassResult.Succeeded)
            {
                throw new HttpListenerException(500, "Password was not changed");
            }

            return ressetPassResult.Succeeded;

        }

        public async Task<bool> UserExists(string username)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username.ToLower());
            if (user == null)
                return false;

            return true;
        }
    }
}
