using DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.DTO;
using System.Threading.Tasks;

namespace PlantTrackerAPI.DataTransferLayer.Interfaces
{
    public interface IAccountService
    {
        public Task<bool> UserExists(string email);
        public Task<UserDTO> RegisterUser(RegisterDTO registerDTO);
        public Task<UserDTO> LogInUser(LoginDTO loginDTO);
        public Task<bool> ForgetPassword(ForgotPasswordRequestDTO forgotPasswordRequestDTO);
        public Task<bool> ResetPassword(ResetPasswordDto resetPasswordDto);
    }
}
