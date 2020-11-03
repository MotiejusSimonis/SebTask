using AutoMapper;
using SEBtask.Models.Dtos;
using SEBtask.Models.Entities;
using SEBtask.Models.Responses;

namespace SEBtask.Profiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Agreement, AgreementDto>().ReverseMap();
            CreateMap<Client, ClientDto>().ReverseMap();
        }
    }
}
