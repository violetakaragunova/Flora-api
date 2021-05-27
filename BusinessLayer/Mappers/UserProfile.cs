using AutoMapper;
using DataTransferLayer.DTO;
using DomainModel.Identity;
using PlantTrackerAPI.DomainModel;

namespace BusinessLayer.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, AppUser>().ReverseMap();
        }
    }
}
