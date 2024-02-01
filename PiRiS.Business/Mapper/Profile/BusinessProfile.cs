using AutoMapper;
using PiRiS.Business.Dto;
using PiRiS.Data.Models;

namespace PiRiS.Business.Mapper;

public class BusinessProfile : Profile
{
    public BusinessProfile()
    {
        CreateMap<ClientDto, Client>();
        CreateMap<Client, ClientDto>();
        CreateMap<Disability, DisabilityDto>();
        CreateMap<Citizenship, CitizenshipDto>();
        CreateMap<City, CityDto>();
        CreateMap<FamilyStatus, FamilyStatusDto>();

        CreateMap<Client, ClientViewDto>()
            .ForMember(x => x.DisabilityStatus, opt => opt.MapFrom(x => x.Disability.DisabilityStatus))
            .ForMember(x => x.FamilyStatus, opt => opt.MapFrom(x => x.FamilyStatus.StatusName))
            .ForMember(x => x.CitizenshipName, opt => opt.MapFrom(x => x.Citizenship.CitizenshipName))
            .ForMember(x => x.CityName, opt => opt.MapFrom(x => x.City.Name));

    }
}
