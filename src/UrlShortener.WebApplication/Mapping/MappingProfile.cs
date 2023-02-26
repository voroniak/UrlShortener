using AutoMapper;
using UrlShortener.Application.Common.Dtos;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UrlManagmentDto, GetUrlModel>().ReverseMap();
        }
    }
}
