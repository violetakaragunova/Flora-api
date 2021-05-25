using AutoMapper;
using DataTransferLayer.DTO;
using StartupWebApplication.Models;

namespace StartupWebApplication.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, UserModel>();
        }
    }
}
