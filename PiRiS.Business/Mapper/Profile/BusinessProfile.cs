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

        CreateMap<DepositPlan, DepositAgreementDto>();

        CreateMap<Client, ClientAgreementDto>();
        CreateMap<Currency, CurrencyDto>();
        CreateMap<DepositCreateDto, Deposit>();

        CreateMap<CreditPlan, CreditPlanAgreementDto>();
        CreateMap<CreditCreateDto, Credit>();

        CreateMap<Deposit, DepositDto>()
            .ForMember(x => x.PlanName, opt => opt.MapFrom(x => x.DepositPlan.Name))
            .ForMember(x=> x.Percent, opt=> opt.MapFrom(x=> x.DepositPlan.Percent))
            .ForMember(x=> x.DepositType, opt=> opt.MapFrom(x=> x.DepositPlan.DepositType))
            .ForMember(x => x.MainAccountNumber, opt => opt.MapFrom(x => x.MainAccount.AccountNumber))
            .ForMember(x => x.PercentAccountNumber, opt => opt.MapFrom(x => x.PercentAccount.AccountNumber));

        CreateMap<Credit, CreditDto>()
            .ForMember(x => x.PlanName, opt => opt.MapFrom(x => x.CreditPlan.Name))
            .ForMember(x=> x.Percent, opt=> opt.MapFrom(x=> x.CreditPlan.Percent))
            .ForMember(x=> x.CreditType, opt=> opt.MapFrom(x=> x.CreditPlan.CreditType))
            .ForMember(x => x.MainAccountNumber, opt => opt.MapFrom(x => x.MainAccount.AccountNumber))
            .ForMember(x => x.PercentAccountNumber, opt => opt.MapFrom(x => x.PercentAccount.AccountNumber));

    }
}
