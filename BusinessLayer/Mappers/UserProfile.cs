using AutoMapper;
using DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DomainModel;

namespace BusinessLayer.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<RegisterDTO, User>();
        }
    }
}
