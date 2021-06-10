using AutoMapper;
using DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DomainModel;
using PlantTrackerAPI.Models;
using StartupWebApplication.Models;

namespace StartupWebApplication.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserModel>().ReverseMap();
            CreateMap<RegisterDTO, UserModel>().ReverseMap();
            CreateMap<LoginDTO, UserModel>().ReverseMap();
            CreateMap<ForgotPasswordRequestDTO, ForgotPasswordModel>().ReverseMap();
            CreateMap<LoginDTO, LoginModel>().ReverseMap();
            CreateMap<RegisterDTO, RegisterModel>().ReverseMap();
            CreateMap<ResetPasswordDto, ResetPasswordModel>().ReverseMap();
        }
    }
}
