using AutoMapper;
using DataTransferLayer.DTO;
using PlantTrackerAPI.DataTransferLayer.DTO;
using PlantTrackerAPI.DomainModel;
using System.Linq;

namespace BusinessLayer.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<PlantImageDTO, PlantImage>().ReverseMap();
            CreateMap<PlantNeedDTO, PlantNeed>();
            CreateMap<PlantNeed,PlantNeedDTO>().ForMember(dest => dest.NeedName, opt =>
                opt.MapFrom(src => src.Need.Name))
                .ForMember(dest => dest.MonthFromName, opt => opt.MapFrom(src => src.MonthFrom.Name))
                .ForMember(dest => dest.MonthToName, opt => opt.MapFrom(src => src.MonthTo.Name))
                .ForMember(dest => dest.FrequencyType, opt => opt.MapFrom(src => src.FrequencyType.Type));
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<RegisterDTO, User>();
            CreateMap<PlantAddDTO, Plant>().ReverseMap();
            CreateMap<PlantDTO, Plant>().ReverseMap()
                .ForMember(dest => dest.PhotoUrl, opt =>
                opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                .ForMember(dest => dest.RoomName, opt =>
               opt.MapFrom(src => src.Room.RoomName));
            CreateMap<NeedDTO, Need>().ReverseMap();
            CreateMap<MonthDTO, Month>().ReverseMap();
            CreateMap<FrequencyTypeDTO, FrequencyType>().ReverseMap();
            CreateMap<ActionDTO, Action>().ReverseMap();
            CreateMap<DashboardPlantDTO, DashboardPlant>().ReverseMap();
            CreateMap<DashboardPlantNeedDTO, DashboardPlantNeed>().ReverseMap();
            CreateMap<RoomDTO,Room>().ReverseMap();
        }
    }
}
