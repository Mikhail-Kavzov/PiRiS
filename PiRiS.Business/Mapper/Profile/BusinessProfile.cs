using AutoMapper;
using PiRiS.Business.Dto.Account;
using PiRiS.Business.Dto.Client;
using PiRiS.Business.Dto.Credit;
using PiRiS.Business.Dto.CreditPlan;
using PiRiS.Business.Dto.Currency;
using PiRiS.Business.Dto.Deposit;
using PiRiS.Business.Dto.DepositPlan;
using PiRiS.Common.Constants;
using PiRiS.Data.Models;
using PiRiS.Data.Models.Enums;

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

        CreateMap<DepositPlan, DepositPlanAgreementDto>()
            .ForMember(x => x.CurrencyName, opt => opt.MapFrom(x => x.Currency.CurrencyName));

        CreateMap<Currency, CurrencyDto>();
        CreateMap<DepositCreateDto, Deposit>();

        CreateMap<CreditPlan, CreditPlanAgreementDto>()
            .ForMember(x => x.CurrencyName, opt => opt.MapFrom(x => x.Currency.CurrencyName));

        CreateMap<CreditCreateDto, Credit>();

        CreateMap<Deposit, DepositDto>()
            .ForMember(x => x.PlanName, opt => opt.MapFrom(x => x.DepositPlan.Name))
            .ForMember(x => x.Percent, opt => opt.MapFrom(x => x.DepositPlan.Percent))
            .ForMember(x => x.DepositType, opt => opt.MapFrom(x => x.DepositPlan.DepositType.ToString()))
            .ForMember(x => x.MainAccountNumber, opt => opt.MapFrom(x => x.MainAccount.AccountNumber))
            .ForMember(x => x.PercentAccountNumber, opt => opt.MapFrom(x => x.PercentAccount.AccountNumber))
            .ForMember(x => x.Surname, opt => opt.MapFrom(x => x.Client.Surname))
            .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Client.LastName))
            .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Client.FirstName))
            .ForMember(x=> x.CurrencyName, opt=> opt.MapFrom(x=> x.DepositPlan.Currency.CurrencyName))
            .ForMember(x => x.DailyProfit, opt => opt.MapFrom(x => (x.Sum * (decimal)x.DepositPlan.Percent) / BankParams.PercentDelimiter / BankParams.DaysInYear))
            .ForMember(x => x.CanClose, opt => opt.Ignore())
            .ForMember(x => x.CanWithdraw, opt => opt.Ignore());

        CreateMap<Credit, CreditDto>()
            .ForMember(x => x.PlanName, opt => opt.MapFrom(x => x.CreditPlan.Name))
            .ForMember(x => x.Percent, opt => opt.MapFrom(x => x.CreditPlan.Percent))
            .ForMember(x => x.CreditType, opt => opt.MapFrom(x => x.CreditPlan.CreditType.ToString()))
            .ForMember(x => x.MainAccountNumber, opt => opt.MapFrom(x => x.MainAccount.AccountNumber))
            .ForMember(x => x.PercentAccountNumber, opt => opt.MapFrom(x => x.PercentAccount.AccountNumber))
            .ForMember(x => x.Surname, opt => opt.MapFrom(x => x.Client.Surname))
            .ForMember(x => x.LastName, opt => opt.MapFrom(x => x.Client.LastName))
            .ForMember(x => x.FirstName, opt => opt.MapFrom(x => x.Client.FirstName))
            .ForMember(x => x.CurrencyName, opt => opt.MapFrom(x => x.CreditPlan.Currency.CurrencyName))
            .ForMember(x => x.CanClose, opt => opt.Ignore())
            .ForMember(x => x.CanPayPercents, opt => opt.Ignore());

        CreateMap<DepositPlanCreateDto, DepositPlan>();
        CreateMap<DepositPlan, DepositPlanDto>()
            .ForMember(x => x.CurrencyName, opt => opt.MapFrom(x => x.Currency.CurrencyName))
            .ForMember(x => x.DepositType, opt => opt.MapFrom(x => x.DepositType.ToString()));

        CreateMap<CreditPlanCreateDto, CreditPlan>();
        CreateMap<CreditPlan, CreditPlanDto>()
            .ForMember(x => x.CurrencyName, opt => opt.MapFrom(x => x.Currency.CurrencyName))
            .ForMember(x => x.CreditType, opt => opt.MapFrom(x => x.CreditType.ToString()));

        CreateMap<Account, AccountDto>()
            .ForMember(x => x.AccountPlanName, opt => opt.MapFrom(x => x.AccountPlan.Name))
            .ForMember(x=> x.AccountPlanCode, opt=> opt.MapFrom(x=> x.AccountPlan.Code))
            .ForMember(x => x.AccountPlanType, opt => opt.MapFrom(x => x.AccountPlan.AccountType.ToString()));

    }
}
