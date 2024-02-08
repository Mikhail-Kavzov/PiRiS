﻿using AutoMapper;
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

        CreateMap<DepositPlan, DepositAgreementDto>();

        CreateMap<Currency, CurrencyDto>();
        CreateMap<DepositCreateDto, Deposit>();

        CreateMap<CreditPlan, CreditPlanAgreementDto>();
        CreateMap<CreditCreateDto, Credit>();

        CreateMap<Deposit, DepositDto>()
            .ForMember(x => x.PlanName, opt => opt.MapFrom(x => x.DepositPlan.Name))
            .ForMember(x => x.Percent, opt => opt.MapFrom(x => x.DepositPlan.Percent))
            .ForMember(x => x.DepositType, opt => opt.MapFrom(x => x.DepositPlan.DepositType))
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
            .ForMember(x=> x.Percent, opt=> opt.MapFrom(x=> x.CreditPlan.Percent))
            .ForMember(x=> x.CreditType, opt=> opt.MapFrom(x=> x.CreditPlan.CreditType))
            .ForMember(x => x.MainAccountNumber, opt => opt.MapFrom(x => x.MainAccount.AccountNumber))
            .ForMember(x => x.PercentAccountNumber, opt => opt.MapFrom(x => x.PercentAccount.AccountNumber));

        CreateMap<DepositPlanCreateDto, DepositPlan>();
        CreateMap<DepositPlan, DepositPlanDto>()
            .ForMember(x => x.CurrencyName, opt => opt.MapFrom(x => x.Currency.CurrencyName));

        CreateMap<CreditPlanCreateDto, CreditPlan>();
        CreateMap<CreditPlan, CreditPlanDto>()
            .ForMember(x => x.CurrencyName, opt => opt.MapFrom(x => x.Currency.CurrencyName));

    }
}
