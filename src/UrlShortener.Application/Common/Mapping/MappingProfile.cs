using AutoMapper;
using UrlShortener.Application.Urls.Queries.GetUrl;
using UrlShortener.Domain.Entities;

namespace UrlShortener.Application.Common.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UrlManagmentDto, UrlManagement>().ReverseMap();
        }
    }
}