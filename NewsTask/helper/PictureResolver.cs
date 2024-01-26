using AutoMapper;
using News.Core.Entities;
using NewsTask.Dtos;

namespace NewsTask.helper
{
    internal class PictureResolver : IValueResolver<NewsEntity, NewsToReturn, string>
    {
        private readonly IConfiguration _configuration;

        public PictureResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(NewsEntity source, NewsToReturn destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Photo))
                return $"{_configuration["PictureBaseURL"]}/{source.Photo}";
            return string.Empty;
        }
    }
}