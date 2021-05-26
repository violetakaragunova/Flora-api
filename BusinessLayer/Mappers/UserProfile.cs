using AutoMapper;
using DataTransferLayer.DTO;
using DomainModel.Identity;

namespace BusinessLayer.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDTO, User>().ReverseMap();
        }
    }
}
