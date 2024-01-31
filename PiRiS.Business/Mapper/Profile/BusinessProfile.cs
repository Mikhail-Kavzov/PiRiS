using AutoMapper;
using PiRiS.Business.Dto;
using PiRiS.Data.Models;

namespace PiRiS.Business.Mapper;

public class BusinessProfile :  Profile
{
    public BusinessProfile() 
    {
        CreateMap<ClientDto,Client>();
        CreateMap<Client, ClientDto>();
    }
}
