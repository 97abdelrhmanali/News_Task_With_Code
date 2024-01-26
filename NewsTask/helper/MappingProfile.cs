using AutoMapper;
using News.Core.Entities;
using NewsTask.Dtos;

namespace NewsTask.helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<NewsEntity, NewsToReturn>()
                .ForMember(d => d.SourceName,o => o.MapFrom(s => s.Source.Name))
                .ForMember(d => d.SourceDescription , o => o.MapFrom(s => s.Source.Description))
                .ForMember(d => d.Photo,o => o.MapFrom<PictureResolver>()).ReverseMap();

            CreateMap<Category, CategoryToReturn>().ReverseMap();

            CreateMap<NewsToUpdate, NewsEntity>();
        }
    }
}
